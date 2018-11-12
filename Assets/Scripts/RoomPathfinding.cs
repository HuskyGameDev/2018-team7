using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public struct PathCellInfo
{
	public bool passable, trigger;
	public int cost;

	public PathCellInfo(bool passable, bool trigger, int cost)
	{
		this.passable = passable;
		this.trigger = trigger;
		this.cost = cost;
	}
}

public class PathNode : IEquatable<PathNode>, IComparable<PathNode>
{
	public Vec2i pos;
	public int g, f, h;
	public PathNode parent;

	public int CompareTo(PathNode other)
	{
		return f.CompareTo(other.f);
	}

	public bool Equals(PathNode other)
		=> pos == other.pos;

	public override string ToString()
	{
		return pos.ToString() + ", F: " + f;
	}
}

public class RoomPathfinding
{
	private Room room;
	private PathCellInfo[,] grid;
	private Queue<PathComputer> data = new Queue<PathComputer>();

	private bool generated = false;

	public RoomPathfinding(Room room)
	{
		this.room = room;
	}

	public void Generate()
	{
		Assert.IsFalse(generated);
		grid = new PathCellInfo[Room.Width, Room.Height];

		for (int y = 0; y < grid.GetLength(1); y++)
		{
			for (int x = 0; x < grid.GetLength(0); x++)
			{
				Vector3 origin = new Vector3(x + 0.5f, y + 0.5f, -100.0f);
				Ray ray = new Ray(origin, Vector3.forward);

				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 200.0f, 0, QueryTriggerInteraction.Collide))
				{
					Collider col = hit.collider;

					if (!col.isTrigger)
						grid[x, y] = new PathCellInfo(false, false, 1);
					else
					{
						TileCollider tC = col.GetComponent<TileCollider>();

						if (tC != null)
						{
							Tile tile = room.GetTile(x, y);
							int scoreMod = tile.id == TileType.Air ? 0 : 100;
							grid[x, y] = new PathCellInfo(true, true, scoreMod);
						}
					}
				}
				else grid[x, y] = new PathCellInfo(true, false, 1);
			}
		}

		generated = true;
	}

	private PathComputer GetData()
	{
		if (data.Count > 0)
			return data.Dequeue();

		return new PathComputer(room, grid);
	}

	public void FindPath(Vec2i start, Vec2i target, Stack<Vector2> path, Action callback)
	{
		if (generated && start != target && room.InBounds(target))
		{
			path.Clear();

			Assert.IsTrue(room.InBounds(start));
			Assert.IsTrue(grid[start.x, start.y].passable);
			Assert.IsTrue(grid[target.x, target.y].passable);

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
