
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static Utils;

public class Floor : MonoBehaviour
{
	// Should this be set at 10?
	public int RoomCount { get; private set; } = 10;

	// Sparse storage. A bit slower, but doesn't matter with our level size. Smaller memory footprint.
	private Dictionary<Vec2i, Room> rooms = new Dictionary<Vec2i, Room>();

	// A pool for reusing sprites. This avoids instantiate/destroy and garbage collection spikes.
	private SpritePool spritePool;
	private ColliderPool colliderPool;

	// Temporary way to access sprites - the tile ID - 1 (index into tile type enum) maps into this.
	// Subtract 1 since air doesn't have a sprite.
	[SerializeField] private TileDataList data;

	public static Floor Instance { get; private set; }

	private FloorGenerator generator;

	private void Awake()
	{
		Instance = this;
		spritePool = GetComponent<SpritePool>();
		colliderPool = GetComponent<ColliderPool>();
		generator = new FloorGenerator(this);
		generator.Generate(RoomCount);
	}

	public TileProperties GetTileProperties(Tile tile)
	{
		return data.GetProperties(tile);
	}

	// Returns the tile at the given location. Location is specified in world tile space.
	public Tile GetTile(int x, int y)
	{
		Vec2i roomP = ToRoomPos(x, y), lP = ToLocalPos(x, y);
		Room room = GetRoom(roomP);
		return room.GetTile(lP.x, lP.y);
	}

	// Sets the given tile at the given location. Location is specified in world tile space.
	public void SetTile(int x, int y, Tile tile)
	{
		Vec2i roomP = ToRoomPos(x, y), localP = ToLocalPos(x, y);
		Room room = GetRoom(roomP);
		room.SetTile(localP.x, localP.y, tile);
	}

	// Returns the room at the given location in room coordinates. Returns null if the room doesn't exist.
	public Room GetRoom(Vec2i roomP)
	{
		Room room;
		if (rooms.TryGetValue(roomP, out room))
			return room;

		return null;
	}

	public Room CreateRoom(int x, int y)
	{
		Vec2i roomP = new Vec2i(x, y);
		Assert.IsTrue(GetRoom(roomP) == null);
		Room room = new Room(x, y, spritePool, colliderPool);
		rooms.Add(roomP, room);
		return room;
	}
}
