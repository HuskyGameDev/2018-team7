
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static Utils;

public class Floor : MonoBehaviour
{
	/// <summary>
	/// The number of rooms in this floor.
	/// </summary>
	public int RoomCount { get; private set; } = 10;

	// The current floor the player is on.
	public int FloorID { get; private set; }

	// Sparse storage. A bit slower, but doesn't matter with our level size. Smaller memory footprint.
	private Dictionary<Vec2i, Room> rooms = new Dictionary<Vec2i, Room>();

	// A pool for reusing sprites. This avoids instantiate/destroy and garbage collection spikes.
	private SpritePool spritePool;
	private ColliderPool colliderPool;

	[SerializeField] private GameObject enemyPrefab;

	// Temporary way to access sprites - the tile ID - 1 (index into tile type enum) maps into this.
	// Subtract 1 since air doesn't have a sprite.
	[SerializeField] private TileDataList data;

	// Singleton instance.
	public static Floor Instance { get; private set; }

	private FloorGenerator generator;

	private void Awake()
	{
		Instance = this;
		spritePool = GetComponent<SpritePool>();
		colliderPool = GetComponent<ColliderPool>();
		generator = new FloorGenerator(this, enemyPrefab);
		Generate();
	}

	/// <summary>
	/// Generate the floor. This creates and builds the rooms that comprise it. 
	/// </summary>
	public void Generate()
	{
		foreach (Room room in rooms.Values)
			room.Destroy();

		rooms.Clear();
		generator.Generate(RoomCount);
		GameObject.FindWithTag("Player").transform.position = new Vector3(5.0f, 5.0f);

		FloorID++;
		Debug.Log("Entering floor " + FloorID);
	}

	/// <summary>
	/// Returns the TileProperties object for the given tile type. This object
	/// contains information about the tile, such as its visibility and collision information.
	/// </summary>
	public TileProperties GetTileProperties(Tile tile)
	{
		return data.GetProperties(tile);
	}

	/// <summary>
	/// Returns the tile at the given location. Location is specified in world tile space.
	/// </summary>
	public Tile GetTile(int x, int y)
	{
		Vec2i roomP = ToRoomPos(x, y), lP = ToLocalPos(x, y);
		Room room = GetRoom(roomP);
		return room.GetTile(lP.x, lP.y);
	}

	/// <summary>
	/// Sets the given tile at the given location. Location is specified in world tile space.
	/// </summary>
	public void SetTile(int x, int y, Tile tile)
	{
		Vec2i roomP = ToRoomPos(x, y), localP = ToLocalPos(x, y);
		Room room = GetRoom(roomP);
		room.SetTile(localP.x, localP.y, tile);
	}

	/// <summary>
	/// Returns the room at the given location in room coordinates. Returns null if the room doesn't exist.
	/// </summary>
	public Room GetRoom(Vec2i roomP)
	{
		Room room;
		if (rooms.TryGetValue(roomP, out room))
			return room;

		return null;
	}

	/// <summary>
	/// Creates a new room at the given room coordinates and adds it to the rooms list for this floor.
	/// </summary>
	public Room CreateRoom(int x, int y)
	{
		Vec2i roomP = new Vec2i(x, y);
		Assert.IsTrue(GetRoom(roomP) == null);
		Room room = new Room(x, y, spritePool, colliderPool);
		rooms.Add(roomP, room);
		return room;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
			Generate();
	}
}
