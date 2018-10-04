using UnityEngine;
using static UnityEngine.Mathf;

public static class Utils
{
	public static readonly Vec2i[] Directions =
	{
		new Vec2i(0, -1), new Vec2i(0, 1), new Vec2i(-1, 0), new Vec2i(1, 0),
		new Vec2i(-1, 1), new Vec2i(1, 1), new Vec2i(-1, -1), new Vec2i(1, -1)
	};

	public static float Square(float v)
	{
		return v * v;
	}

	public static int Square(int v)
	{
		return v * v;
	}

	// Converts world (Unity units) coordinates into room coordinates.
	public static Vec2i ToRoomPos(int x, int y)
	{
		return new Vec2i(x >> Room.ShiftX, FloorToInt((float)y / Room.Height));
	}

	public static Vec2i ToRoomPos(Vec2i pos)
	{
		return ToRoomPos(pos.x, pos.y);
	}

	public static Vec2i ToRoomPos(Vector2 pos)
	{
		return ToRoomPos(RoundToInt(pos.x), RoundToInt(pos.y));
	}

	// Converts world (Unity units) coordinates into local coordinates. 
	// Local coordinates are between 0 and the size of a room.
	public static Vec2i ToLocalPos(int x, int y)
	{
		return new Vec2i(x & Room.MaskX, y % Room.Height);
	}

	public static Vec2i ToLocalPos(Vec2i p)
	{
		return ToLocalPos(p.x, p.y);
	}

	// Given a room position and local coordinates within it, returns the world tile position.
	public static Vec2i ToTilePos(Vec2i roomPos, int lX, int lY)
	{
		return new Vec2i(roomPos.x * Room.Width + lX, roomPos.y * Room.Height + lY);
	}

	public static Vec2i TilePos(float x, float y)
	{
		return new Vec2i(RoundToInt(x), RoundToInt(y));
	}

	public static Vec2i TilePos(Vector2 p)
	{
		return TilePos(p.x, p.y);
	}

	public static void Swap<T>(ref T a, ref T b)
	{
		T temp = a;
		a = b;
		b = temp;
	}
}
