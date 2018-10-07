using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class Room
{
	// It's more efficient if height is a power of 2, but it probably doesn't matter. This way, if we use 32 
	// pixels per unit and 16:9 resolution, we can fit 32x18 tiles on the screen. This of course depends on 
	// the aspect ratio we decide on.
	public const int Width = 32, Height = 18;

	public const int HalfSizeX = Width / 2, HalfSizeY = Height / 2;
	public const int LimX = Width - 1, LimY = Height - 1;

	// Used to optimize division/remainder operations given the width is a power of 2.
	public const int ShiftX = 5, MaskX = Width - 1;

	private Tile[] tiles;

	public bool hasSprites = false;

	private List<SpriteRenderer> spriteList = new List<SpriteRenderer>();

	// References to floor variables.
	private SpritePool spritePool;
	private RoomCollision collision;

	public Vec2i Pos { get; private set; }
	public Vector2 WorldPos { get; private set; }

	public Room(int pX, int pY, SpritePool spritePool, ColliderPool colliderPool)
	{
		tiles = new Tile[Width * Height];

		Pos = new Vec2i(pX, pY);
		WorldPos = new Vector2(pX * Width, pY * Height);

		this.spritePool = spritePool;
		collision = new RoomCollision(this, colliderPool);
	}

	// Returns a tile at the given location from this room. Fails if the location is out of bounds of the room.
	// Coordinates are specified in local room space between 0 and room size - 1. 
	public Tile GetTile(int x, int y)
	{
		Assert.IsTrue(InBounds(x, y));
		return tiles[y * Width + x];
	}

	// Sets a tile at the given location from this room. Fails if the location is out of bounds of the room.
	// Coordinates are specified in local room space between 0 and room size - 1.
	public void SetTile(int x, int y, Tile tile)
	{
		Assert.IsTrue(InBounds(x, y));
		tiles[y * Width + x] = tile;
	}

	// Returns true if the given coordinates are within the boundaries of this room.
	// Coordinates are specified in local room space between 0 and room size - 1.
	public static bool InBounds(int x, int y)
	{
		return x >= 0 && x < Width && y >= 0 && y < Height;
	}

	// Draw all tiles in this room by setting sprite renderers at their locations from the pool.
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

	public void RemoveSprites()
	{
		for (int i = 0; i < spriteList.Count; i++)
			spritePool.Return(spriteList[i]);

		spriteList.Clear();
		hasSprites = false;
	}

	public void SetColliders()
	{
		collision.Generate();
	}

	public void RemoveColliders()
	{
		collision.RemoveColliders();
	}

	public void Destroy()
	{
		RemoveColliders();
		RemoveSprites();
	}
}
