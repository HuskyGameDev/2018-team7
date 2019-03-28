using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class FloorGenerator
{
	protected ItemSpawner spawner;
	protected GameObject[] enemyPrefabs;

	// Used for connecting two rooms together via a pathway.
	// Stores the axis the pathway will exist on as well as positions 
	// for the pathway.
	protected struct Connection
	{
		public Vec2i a, b;
		public bool xAxis;

		public Connection(Vec2i a, Vec2i b, bool xAxis)
		{
			this.a = a;
			this.b = b;
			this.xAxis = xAxis;
		}
	}

	protected delegate void PatternFunc(Room room);

	protected PatternFunc[] patterns;

	protected Floor floor;

	public FloorGenerator(Floor floor, GameObject[] enemyPrefabs)
	{
		this.floor = floor;
		this.enemyPrefabs = enemyPrefabs;
		spawner = GameObject.FindWithTag("ItemSpawner").GetComponent<ItemSpawner>();

		patterns = new PatternFunc[4];
		patterns[0] = NormalPattern;
		patterns[1] = ExtraObstacles;
		patterns[2] = WallsPattern;
		patterns[3] = XPattern;
	}

	protected void AddBase(Room room)
	{
		// Add top and bottom walls.
		for (int x = 1; x <= Room.LimX - 1; x++)
		{
			room.SetTile(x, Room.LimY, TileType.Wall);
			room.SetTile(x, 0, TileType.Wall);
		}

		// Add left and right walls.
		for (int y = 1; y <= Room.LimY - 1; y++)
		{
			room.SetTile(0, y, TileType.Wall);
			room.SetTile(Room.LimX, y, TileType.Wall);
		}

		// Add corner walls. These could be baked into the above two loops, but I left them separate
		// since it depends on the art complexity.
		room.SetTile(0, Room.LimY, TileType.Wall);
		room.SetTile(Room.LimX, Room.LimY, TileType.Wall);
		room.SetTile(0, 0, TileType.Wall);
		room.SetTile(Room.LimX, 0, TileType.Wall);

		// Add floor.
		for (int y = 1; y <= Room.LimY - 1; y++)
		{
			for (int x = 1; x <= Room.LimX - 1; x++)
				room.SetTile(x, y, TileType.Floor);
		}
	}

	// Room pattern with some obstacles.
	protected void NormalPattern(Room room)
	{
		AddBase(room);

		int obstacleCount = Random.Range(0, 6);

		// Add some random obstacles for collision testing.
		for (int i = 0; i < obstacleCount; i++)
		{
			int x = Random.Range(2, Room.LimX - 1);
			int y = Random.Range(2, Room.LimY - 1);
			room.SetTile(x, y, TileType.Wall);
		}
	}

	// Normal pattern but with more obstacle walls.
	protected void ExtraObstacles(Room room)
	{
		AddBase(room);

		int obstacleCount = Random.Range(10, 25);

		for (int i = 0; i < obstacleCount; i++)
		{
			int x = Random.Range(2, Room.LimX - 1);
			int y = Random.Range(2, Room.LimY - 1);
			room.SetTile(x, y, TileType.Wall);
		}
	}

	// Adds random linear walls to the room.
	protected void WallsPattern(Room room)
	{
		AddBase(room);

		int count = Random.Range(2, 4);

		for (int i = 0; i < count; i++)
		{
			bool vertical = Random.value < 0.5f;

			int startX = Random.Range(2, 12);
			int startY = Random.Range(2, 8);

			if (vertical)
			{
				int dist = Random.Range(4, 9);

				for (int j = startY; j < startY + dist; j++)
					room.SetTile(startX, j, TileType.Wall);
			}
			else
			{
				int dist = Random.Range(8, 15);

				for (int j = startX; j < startX + dist; j++)
					room.SetTile(j, startY, TileType.Wall);
			}
		}
	}

	// Adds an x-pattern of walls in the room.
	protected void XPattern(Room room)
	{
		AddBase(room);
		int y = Room.Height - 3;

		int start = Random.Range(2, 5);
		int end = start + 10;

		for (int x = start; x < end; x++, y--)
		{
			room.SetTile(x, y, TileType.Wall);
			room.SetTile(x, Room.Height - 1 - y, TileType.Wall);
			room.SetTile(Room.Width - 1 - x, y, TileType.Wall);
			room.SetTile(Room.Width - 1 - x, Room.Height - 1 - y, TileType.Wall);
		}
	}

	// Returns a random position in the room not blocked by 
	// an obstacle, and inset by 4 tiles from each wall.
	protected Vector2 RandomFreePosition(Room room)
	{
		int x, y;

		do
		{
			x = Random.Range(4, Room.Width - 4);
			y = Random.Range(4, Room.Height - 4);
		}
		while (room.GetTile(x, y) != TileType.Floor);

		return room.WorldPos + new Vector2(x, y);
	}

	protected virtual Vec2i GetNextPos(Vec2i current)
	{
		return Vec2i.Zero;
	}

	private void SpawnEnemy(EnemyType type, Room room, Vector2 pos)
	{
		GameObject enemy = Object.Instantiate(enemyPrefabs[(int)type], pos, Quaternion.identity);
		enemy.GetComponent<Enemy>().room = room;
		enemy.GetComponent<EnemyLoot>()?.SetWeaponDrop();
		room.AddEnemy(enemy);
	}

	private PatternFunc GetRoomPattern()
	{
		float v = Random.value;

		if (v < 0.1f)
			return patterns[3];
		else return patterns[Random.Range(0, patterns.Length - 1)];
	}

	protected virtual void BuildRoom(Room room, bool stairRoom, bool powerupRoom)
	{
		GetRoomPattern().Invoke(room);

		if (stairRoom)
			room.hasStairs = true;

		Vector2 wPos = room.WorldPos;

		if (powerupRoom)
			spawner.SpawnItem(RandomFreePosition(room));

		int enemyCount = Random.Range(3, 6);

		for (int i = 0; i < enemyCount; i++)
		{
			float val = Random.value;
			EnemyType type;

			if (val < 0.1f)
				type = EnemyType.Patrol;
			else if (val < 0.2f)
				type = EnemyType.Sentry;
			else if (val < 0.4f)
				type = EnemyType.Bomber;
			else type = EnemyType.Helicopter;

			SpawnEnemy(type, room, RandomFreePosition(room));
		}

		room.Lock();
	}

	/// <summary>
	/// Generates the specified number of rooms.
	/// </summary>
	public virtual void Generate() { }

	private struct PathOrder
	{
		public TileType a, b;

		public PathOrder(TileType a, TileType b)
		{
			this.a = a;
			this.b = b;
		}
	}

	protected void AddConnections(List<Connection> connections)
	{
		for (int c = 0; c < connections.Count; c++)
		{
			Connection info = connections[c];

			Vec2i a = info.a * new Vec2i(Room.Width, Room.Height);
			Vec2i b = info.b * new Vec2i(Room.Width, Room.Height);

			PathOrder order = new PathOrder(TileType.TempWall, TileType.Floor);

			if (info.xAxis)
			{
				if (b.x < a.x)
				{
					Swap(ref a, ref b);
					Swap(ref order.a, ref order.b);
				}

				int startX = a.x + Room.LimX, y = a.y + Room.HalfSizeY;

				floor.SetTile(startX, y, order.a);
				floor.SetTile(startX + 1, y, order.b);
			}
			else
			{
				if (b.y < a.y)
				{
					Swap(ref a, ref b);
					Swap(ref order.a, ref order.b);
				}

				int startY = a.y + Room.LimY, x = a.x + Room.HalfSizeX;

				floor.SetTile(x, startY, order.a);
				floor.SetTile(x, startY + 1, order.b);
			}
		}
	}
}
