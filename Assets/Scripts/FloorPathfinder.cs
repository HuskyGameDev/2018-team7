using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System;
using System.Threading;

public sealed class FloorPathfinder
{
	private Floor floor;
	private PathCellInfo[,] grid;
	private Queue<PathComputer> data = new Queue<PathComputer>();

	public FloorPathfinder(Floor floor)
	{
		this.floor = floor;
	}

	public void Generate()
	{
		int width = (floor.MaxRoom.x + 1) * Room.Width;
		int height = (floor.MaxRoom.y + 1) * Room.Height;

		grid = new PathCellInfo[width, height];
		UpdateArea(0, 0, width, height);
	}

	public void UpdateArea(int startX, int startY, int endX, int endY)
	{
		for (int y = startY; y < endY; y++)
		{
			for (int x = startX; x < endX; x++)
			{
				Tile tile = floor.GetTile(x, y);

				TileProperties props = tile.Properties;
				bool trigger = props.trigger;
				bool passable = !props.hasCollider || trigger;

				int cost = 0;

				if (!passable)
					cost = 1000;
				else cost = trigger ? 100 : 1;

				grid[x, y] = new PathCellInfo(passable, trigger, cost);
			}
		}
	}

	public void PrintCellInfoAtCursor()
	{
		Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vec2i tile = Utils.TilePos(world);
		Debug.Log(grid[tile.x, tile.y]);
	}

	public bool InBounds(Vec2i p)
	{
		return p.x >= 0 && p.y >= 00 && p.x < grid.GetLength(0) && p.y < grid.GetLength(1);
	}

	private PathComputer GetData()
	{
		if (data.Count > 0)
			return data.Dequeue();

		return new PathComputer(this, grid);
	}

	public void FindPath(Vec2i start, Vec2i target, Stack<Vector2> path, Action callback)
	{
		if (start != target && InBounds(target))
		{
			path.Clear();

			Assert.IsTrue(InBounds(start));
			Assert.IsTrue(grid[start.x, start.y].passable);
			Assert.IsTrue(grid[target.x, target.y].passable);

			// This allows for multithreading this in the future if necessary - a different PathComputer
			// instance must be used per thread to avoid thread contention.
			PathComputer d = GetData();

			d.SetInfo(start, target, path);
			d.FindPath(callback);
		}
	}

	public bool EmptyCell(int x, int y)
	{
		return grid[x, y].passable && !grid[x, y].trigger;
	}
}
