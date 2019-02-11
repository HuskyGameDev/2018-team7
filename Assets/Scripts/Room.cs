using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class Room
{
	// It's more efficient if height is a power of 2, but it probably doesn't matter. This way, if we use 32 
	// pixels per unit and 16:9 resolution, we can fit 32x18 tiles on the screen. This of course depends on 
	// the aspect ratio we decide on.

	/// <summary>
	/// The width of the room, in tiles.
	/// </summary>
	public const int Width = 32;

	/// <summary>
	/// The height of the room, in tiles.
	/// </summary>
	public const int Height = 18;

	/// <summary>
	/// Half the width of a room, in tiles.
	/// </summary>
	public const int HalfSizeX = Width / 2;

	/// <summary>
	/// Half the height of a room, in tiles.
	/// </summary>
	public const int HalfSizeY = Height / 2;

	/// <summary>
	/// The room's limit on the x-axis, equivalent to the actual width - 1. This is used
	/// when looping as the final value (considering 0 indexing).
	/// </summary>
	public const int LimX = Width - 1;

	/// <summary>
	/// The room's limit on the y-axis, equivalent to the actual width - 1. This is used
	/// when looping as the final value (considering 0 indexing).
	/// </summary>
	public const int LimY = Height - 1;

	/// <summary>
	/// Used to optimize division. Saying x >> ShiftX is the same 
	/// as saying x / Room.SizeX and truncating the result to an integer.
	/// </summary>
	public const int ShiftX = 5;

	/// <summary>
	/// Used for efficient remainder operations. It is technically the same as LimX, but I
	/// keep it a separate value to show intent. Saying x & MaskX is the same as saying
	/// x % MaskX + 1.
	/// </summary>
	public const int MaskX = Width - 1;

	private Tile[] tiles;

	private RectInt tileRect;

	/// <summary>
	/// If true, this room has sprites and is being rendered.
	/// </summary>
	public bool hasSprites = false;

	// Stores all sprites this room is using so that they can be returned during the unload process.
	private List<SpriteRenderer> spriteList = new List<SpriteRenderer>();

	// References to floor variables.
	private SpritePool spritePool;
	private RoomCollision collision;

	/// <summary>
	/// The room's position, in room coordinates.
	/// </summary>
	public Vec2i Pos { get; private set; }

	/// <summary>
	/// The room's world position in Unity world space.
	/// </summary>
	public Vector2 WorldPos { get; private set; }

	// If true, this room cannot be exited until all enemies in it are killed.
	private bool locked;

	private List<EnemyController> enemies = new List<EnemyController>();
	private bool enemiesActive = false;

	private Floor floor;

	public Room(int pX, int pY, SpritePool spritePool, ColliderPool colliderPool, Floor floor)
	{
		this.floor = floor;
		tiles = new Tile[Width * Height];

		Pos = new Vec2i(pX, pY);
		tileRect = new RectInt(pX * Width, pY * Height, Width, Height);
		WorldPos = new Vector2(tileRect.xMin, tileRect.yMin);

		this.spritePool = spritePool;

		collision = new RoomCollision(this, colliderPool);
	}

	/// <summary>
	/// Returns a tile at the given location from this room. Fails if the location is out of bounds of the room.
	/// Coordinates are specified in local room space between 0 and room size - 1. 
	/// </summary>
	public Tile GetTile(int x, int y)
	{
		Assert.IsTrue(InBounds(x, y));
		return tiles[y * Width + x];
	}

	/// <summary>
	/// Sets a tile at the given location from this room. Fails if the location is out of bounds of the room.
	/// Coordinates are specified in local room space between 0 and room size - 1.
	/// </summary>
	public void SetTile(int x, int y, Tile tile)
	{
		Assert.IsTrue(InBounds(x, y));
		tiles[y * Width + x] = tile;
	}

	public void AddEnemy(GameObject enemy)
	{
		Assert.IsNotNull(enemy.GetComponent<EnemyController>());
		enemies.Add(enemy.GetComponent<EnemyController>());
	}

	/// <summary>
	/// Makes all entities linked to this room become active. This causes them to update
	/// their AI and act.
	/// </summary>
	public void ActivateEnemies()
	{
		if (!enemiesActive)
		{
			for (int i = 0; i < enemies.Count; i++)
				enemies[i].enabled = true;

			enemiesActive = true;
		}
	}

	/// <summary>
	/// // Returns true if the given coordinates are within the boundaries of this room.
	/// Coordinates are specified in local room space between 0 and room size - 1.
	/// </summary>
	public static bool InBounds(int x, int y)
	{
		return x >= 0 && x < Width && y >= 0 && y < Height;
	}

	/// <summary>
	/// Draw all tiles in this room by setting sprite renderers at their locations from the pool.
	/// </summary>
	public void SetSprites()
	{
		if (!hasSprites)
		{
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					Tile tile = GetTile(x, y);
					TileProperties data = tile.Properties;

					if (tile != TileType.Air)
					{
						SpriteRenderer rend = spritePool.Get();
						rend.sprite = data.sprite;
						rend.transform.position = WorldPos + new Vector2(x, y);

						spriteList.Add(rend);
					}
				}
			}

			hasSprites = true;
		}
	}

	private void Rebuild()
	{
		RemoveSprites();
		RemoveColliders();
		SetSprites();
		SetColliders();
		floor.Pathfinder.UpdateArea(tileRect.xMin, tileRect.yMin, tileRect.xMax, tileRect.yMax);
	}

	public void Lock()
	{
		locked = true;
	}

	private void Unlock()
	{
		for (int i = 0; i < tiles.Length; i++)
		{
			if (tiles[i].id == TileType.TempWall)
				tiles[i] = TileType.Floor;
		}

		locked = false;
		Rebuild();
	}

	public void CheckForUnlock()
	{ 
		if (locked)
		{
			for (int i = enemies.Count - 1; i >= 0; i--)
			{
				if (enemies[i] == null)
					enemies.RemoveAt(i);
			}

			if (enemies.Count == 0)
				Unlock();
		}
	}

	/// <summary>
	/// Remove all sprites from this room. It will no longer be visible.
	/// </summary>
	public void RemoveSprites()
	{
		for (int i = 0; i < spriteList.Count; i++)
			spritePool.Return(spriteList[i]);

		spriteList.Clear();
		hasSprites = false;
	}

	/// <summary>
	/// Set colliders for this room. Once called, entities will be able to collide with the room.
	/// </summary>
	public void SetColliders()
	{
		collision.Generate();
	}

	/// <summary>
	/// Remove colliders for this room. Once called, entities will no longer be able to collide with the room.
	/// </summary>
	public void RemoveColliders()
	{
		collision.RemoveColliders();
	}

	/// <summary>
	/// Destroys this room, removing its colliders and sprites.
	/// </summary>
	public void Destroy()
	{
		RemoveColliders();
		RemoveSprites();
	}

	public bool InBounds(Vec2i p)
	{
		return p.x >= 0 && p.y >= 0 && p.x < Width && p.y < Height;
	}
}
