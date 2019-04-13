
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static Utils;

public class Floor : MonoBehaviour
{
	// The current floor the player is on.
	public int FloorID { get; private set; } = 1;

	// Sparse storage. A bit slower, but doesn't matter with our level size. Smaller memory footprint.
	private Dictionary<Vec2i, Room> rooms = new Dictionary<Vec2i, Room>();

	// A pool for reusing sprites. This avoids instantiate/destroy and garbage collection spikes.
	private SpritePool spritePool;
	private ColliderPool colliderPool;

	[SerializeField] private GameObject[] enemyPrefabs;

	// Temporary way to access sprites - the tile ID - 1 (index into tile type enum) maps into this.
	// Subtract 1 since air doesn't have a sprite.
	[SerializeField] private TileDataList data;

	// Currently the min room is always (0, 0) and the max room is the last room generated.
	// This could change if we make the generator more sophisticated.
	public Vec2i MaxRoom { get; set; }

	public FloorPathfinder Pathfinder { get; private set; }

	// Singleton instance.
	public static Floor Instance { get; private set; }

	private FloorGenerator[] generators = new FloorGenerator[1];
	private FloorGenerator generator;

	public SaveData saveData { get; set; }

    private bool firstTime = true;

    private void Awake()
	{
		Instance = this;

		spritePool = GetComponent<SpritePool>();
		colliderPool = GetComponent<ColliderPool>();

		generators[0] = new LinearGenerator(this, enemyPrefabs);

		Pathfinder = new FloorPathfinder(this);
	}

	private void Start()
	{
		if (saveData != null)
			Generate(saveData.floor ?? 1);
		else Generate(FloorID);
	}

	public void Destroy()
	{
		foreach (Room room in rooms.Values)
			room.Destroy();

		rooms.Clear();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

		for (int i = 0; i < enemies.Length; i++)
			Destroy(enemies[i]);

		for (int i = 0; i < pickups.Length; i++)
			Destroy(pickups[i]);
	}

	// Clears all tiles immediately around the given tile position to floor tiles.
	private void ClearAround(Vec2i p)
	{
		Vec2i roomP = ToRoomPos(p);
		Vec2i local = ToLocalPos(p);

		Room room = GetRoom(roomP);

		for (int y = -1; y <= 1; y++)
		{
			for (int x = -1; x <= 1; x++)
				room.SetTile(local.x + x, local.y + y, TileType.Floor);
		}
	}

	/// <summary>
	/// Generate the floor. This creates and builds the rooms that comprise it. 
	/// </summary>
	public void Generate(int floorID)
	{
		FloorID = floorID;

		// Seeds the random generator. Use the base seed for the level + the floor ID to
		// offset the seed by the floor. By doing so, we'll get a different generation 
		// pattern for each successive floor in a way that is easily reproducible 
		// in a save/load situation.
		Random.InitState((GameController.seed + floorID) % GameController.MaxSeed);

		generator = generators[Random.Range(0, generators.Length)];
		generator.Generate();

		// Ensure the player cannot spawn inside of walls by clearing any nearby walls.
		// TODO: We need to make sure there's no case where the player will get trapped from this.
		ClearAround(new Vec2i(5, 5));

		GameObject.FindWithTag("Player").transform.position = new Vector3(5.0f, 5.0f);
        if (firstTime)
            firstTime = false;//Don't
        else
            GameController.Instance.AddScore(10);
		
		Pathfinder.Generate();
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

		if (room == null) return TileType.Air;

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
	/// Note: As is, rooms cannot be negative due to the pathfinding code, though the dictionary we use
	/// to store rooms supports negatives.
	/// </summary>
	public Room CreateRoom(int x, int y)
	{
		Assert.IsTrue(x >= 0 && y >= 0);
		Vec2i roomP = new Vec2i(x, y);
		Assert.IsTrue(GetRoom(roomP) == null);
		Room room = new Room(x, y, spritePool, colliderPool, this);
		rooms.Add(roomP, room);
		return room;
	}
}
