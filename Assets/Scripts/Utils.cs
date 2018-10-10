using UnityEngine;
using static UnityEngine.Mathf;

public static class Utils
{
	/// <summary>
	/// Stores directions in the order given in the Direction class:
	/// Back, front, left, right, back-front, front-right, left-back, right-back.
	/// </summary>
	public static readonly Vec2i[] Directions =
	{
		new Vec2i(0, -1), new Vec2i(0, 1), new Vec2i(-1, 0), new Vec2i(1, 0),
		new Vec2i(-1, 1), new Vec2i(1, 1), new Vec2i(-1, -1), new Vec2i(1, -1)
	};

	/// <summary>
	/// Returns the value squared.
	/// </summary>
	public static float Square(float v)
	{
		return v * v;
	}

	/// <summary>
	/// Returns the value squared.
	/// </summary>
	public static int Square(int v)
	{
		return v * v;
	}

	/// <summary>
	/// Converts world (Unity units) coordinates into room coordinates.
	/// </summary>
	public static Vec2i ToRoomPos(int x, int y)
	{
		return new Vec2i(x >> Room.ShiftX, FloorToInt((float)y / Room.Height));
	}

	/// <summary>
	/// Converts world (Unity units) coordinates into room coordinates.
	/// </summary>
	public static Vec2i ToRoomPos(Vec2i pos)
	{
		return ToRoomPos(pos.x, pos.y);
	}

	/// <summary>
	/// Converts world (Unity units) coordinates into room coordinates.
	/// </summary>
	public static Vec2i ToRoomPos(Vector2 pos)
	{
		return ToRoomPos(RoundToInt(pos.x), RoundToInt(pos.y));
	}

	/// <summary>
	/// Converts world (Unity units) coordinates into local coordinates. 
	/// Local coordinates are between 0 and the size of a room.
	/// </summary>
	public static Vec2i ToLocalPos(int x, int y)
	{
		return new Vec2i(x & Room.MaskX, y % Room.Height);
	}

	/// <summary>
	/// Converts world (Unity units) coordinates into local coordinates. 
	/// Local coordinates are between 0 and the size of a room.
	/// </summary>
	public static Vec2i ToLocalPos(Vec2i p)
	{
		return ToLocalPos(p.x, p.y);
	}

	/// <summary>
	/// Given a room position and local coordinates within it, returns the world tile position.
	/// </summary>
	public static Vec2i ToTilePos(Vec2i roomPos, int lX, int lY)
	{
		return new Vec2i(roomPos.x * Room.Width + lX, roomPos.y * Room.Height + lY);
	}

	/// <summary>
	/// Converts the given world position to a tile position by rounding the values to
	/// the nearest integer.
	/// </summary>
	public static Vec2i TilePos(float x, float y)
	{
		return new Vec2i(RoundToInt(x), RoundToInt(y));
	}

	/// <summary>
	/// Converts the given world position to a tile position by rounding the values to
	/// the nearest integer.
	/// </summary>
	public static Vec2i TilePos(Vector2 p)
	{
		return TilePos(p.x, p.y);
	}

	/// <summary>
	/// Swaps the two values.
	/// </summary>
	public static void Swap<T>(ref T a, ref T b)
	{
		T temp = a;
		a = b;
		b = temp;
	}
}
