﻿using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class FloorGenerator
{
	protected ItemSpawner spawner;
	protected GameObject[] enemyPrefabs;

    protected TileType wallType, floorType;

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

		patterns = new PatternFunc[5];
		patterns[0] = NormalPattern;
		patterns[1] = ExtraObstacles;
		patterns[2] = WallsPattern;
		patterns[3] = XPattern;
		patterns[4] = BossPattern;
	}

	protected void AddBase(Room room)
	{
		// Add top and bottom walls.
		for (int x = 1; x <= Room.LimX - 1; x++)
		{
			room.SetTile(x, Room.LimY, wallType);
			room.SetTile(x, 0, wallType);
		}

		// Add left and right walls.
		for (int y = 1; y <= Room.LimY - 1; y++)
		{
			room.SetTile(0, y, wallType);
			room.SetTile(Room.LimX, y, wallType);
		}

		// Add corner walls. These could be baked into the above two loops, but I left them separate
		// since it depends on the art complexity.
		room.SetTile(0, Room.LimY, wallType);
		room.SetTile(Room.LimX, Room.LimY, wallType);
		room.SetTile(0, 0, wallType);
		room.SetTile(Room.LimX, 0, wallType);

		// Add floor.
		for (int y = 1; y <= Room.LimY - 1; y++)
		{
			for (int x = 1; x <= Room.LimX - 1; x++)
				room.SetTile(x, y, floorType);
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
			room.SetTile(x, y, wallType);
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
			room.SetTile(x, y, wallType);
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
			int startY = Random.Range(3, 8);

			if (vertical)
			{
				int dist = Random.Range(4, 9);

				for (int j = startY; j < startY + dist; j++)
					room.SetTile(startX, j, wallType);
			}
			else
			{
				int dist = Random.Range(8, 15);

				for (int j = startX; j < startX + dist; j++)
					room.SetTile(j, startY, wallType);
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
			room.SetTile(x, y, wallType);
			room.SetTile(x, Room.Height - 1 - y, wallType);
			room.SetTile(Room.Width - 1 - x, y, wallType);
			room.SetTile(Room.Width - 1 - x, Room.Height - 1 - y, wallType);
		}
	}

	protected void BossPattern(Room room)
	{
		AddBase(room);
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
		while (room.GetTile(x, y) != floorType);

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

		if (type == EnemyType.Boss)
			enemy.GetComponent<bossscript>().CreatePhase1Spots(room);
	}

	private PatternFunc GetRoomPattern(bool endRoom)
	{
        if (Random.value < 0.5f)
        {
            wallType = TileType.ColoredWall;
            floorType = TileType.ColoredFloor;
        }
        else
        {
            wallType = TileType.Wall;
            floorType = TileType.Floor;
        }

		if (endRoom)
			return patterns[patterns.Length - 1];
		else
		{
			float v = Random.value;

			if (v < 0.1f)
				return patterns[3];
			else return patterns[Random.Range(0, patterns.Length - 2)];
		}
	}

	private void SpawnEnemies(Room room)
	{
		int minCount = 1 + (2 * floor.FloorID);
		int maxCount = 3 + (2 * floor.FloorID);

		int enemyCount = Mathf.Min(Random.Range(minCount, maxCount), 15);

		for (int i = 0; i < enemyCount; i++)
		{
			float val = Random.value;
			EnemyType type;

			if (val < 0.4f) // 40% chance of helicopter
				type = EnemyType.Helicopter;
			else if (val < 0.55f) // 15% chance of sentry
				type = EnemyType.Sentry;
			else if (val < 0.7f) // 15% chance of patrol
				type = EnemyType.Patrol;
			else if (val < 0.8f) // 10% chance of assault patrol
				type = EnemyType.AssaultPatrol;
			else if (val < 0.85f) // 5% chance of shotgun patrol
				type = EnemyType.ShotgunPatrol;
			else if (val < 0.87f) // 2% chance of tank
				type = EnemyType.Tank; 
			else type = EnemyType.Bomber; // 13% chance of bomber

			SpawnEnemy(type, room, RandomFreePosition(room));
		}
	}

	private void SpawnBoss(Room room)
	{
		SpawnEnemy(EnemyType.Boss, room, room.WorldPos + new Vector2(Room.Width / 2, Room.Height / 2));
	}

	protected virtual void BuildRoom(Room room, bool stairRoom, bool powerupRoom)
	{
		bool endRoom = stairRoom;

		GetRoomPattern(endRoom).Invoke(room);

		if (stairRoom)
			room.hasStairs = true;

		Vector2 wPos = room.WorldPos;

		if (powerupRoom)
			spawner.SpawnItem(RandomFreePosition(room));

		if (endRoom && floor.FloorID % 2 == 0)
			SpawnBoss(room);
		else
			SpawnEnemies(room);

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

			PathOrder order = new PathOrder(TileType.TempWall, floorType);

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
