//
// Copyright (c) 2018 Jason Bricco
//

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Threading;
using System;

public sealed class PathComputer
{
	private PathCellInfo[,] grid;

	private Vec2i start;
	private Vec2i target;
	private Stack<Vector2> path;

	private FloorPathfinder floor;
	private PathNode[,] nodes;

	private SortedList<Vec2i, PathNode> openList = new SortedList<Vec2i, PathNode>();
	private HashSet<PathNode> closedList = new HashSet<PathNode>();

	private int successorCount = 0;
	private PathNode[] successors = new PathNode[8];

	public PathComputer(FloorPathfinder floor, PathCellInfo[,] grid)
	{
		this.floor = floor;
		this.grid = grid;

		nodes = new PathNode[grid.GetLength(0), grid.GetLength(1)];
	}

	public void SetInfo(Vec2i start, Vec2i target, Stack<Vector2> path)
	{
		this.start = start;
		this.target = target;
		this.path = path;
	}

	private PathNode GetNode(Vec2i p)
	{
		PathNode node = nodes[p.x, p.y];

		if (node == null)
		{
			node = new PathNode();
			nodes[p.x, p.y] = node;
		}

		node.pos = p;
		return node;
	}

	private void GetSuccessors(PathNode current, Vec2i pos)
	{
		successorCount = 0;

		for (int i = 0; i < 4; i++)
		{
			Vec2i next = pos + Vec2i.Directions[i];

			if (floor.InBounds(next) && grid[next.x, next.y].passable)
				successors[successorCount++] = GetNode(next);
		}

		for (int i = 4; i < 8; i++)
		{
			Vec2i dir = Vec2i.Directions[i];
			Vec2i next = pos + dir;

			if (floor.InBounds(next))
			{
				PathCellInfo diag = grid[next.x, next.y];

				if (diag.passable)
				{
					PathCellInfo adjX = grid[pos.x + dir.x, pos.y];
					PathCellInfo adjY = grid[pos.x, pos.y + dir.y];

					if (adjX.passable && !adjX.trigger && adjY.passable && !adjY.trigger)
						successors[successorCount++] = GetNode(next);
				}
			}
		}
	}

	private void TracePath(PathNode dest)
	{
		PathNode current = dest;

		while (current.pos != start)
		{
			path.Push(new Vector2(current.pos.x, current.pos.y));
			current = current.parent;
			Assert.IsNotNull(current);
		}
	}

	// Compute the estimated number of cells to reach the destination 
	// using Manhattan distance.
	private int ComputeHeuristic(Vec2i start, Vec2i end)
		=> Mathf.Abs(end.x - start.x) + Mathf.Abs(end.y - start.y);


	// This could be multithreaded, but we would have to address thread contention.
	// ThreadPool.QueueUserWorkItem(FindPath, callback);
	public void FindPath(Action callback)
	{
		try
		{
			openList.Add(start, GetNode(start));

			while (openList.Count > 0)
			{
				PathNode current = openList.First;
				openList.RemoveFirst(current.pos, current);
				closedList.Add(current);

				if (current.pos == target)
				{
					TracePath(current);
					callback.Invoke();
					return;
				}

				GetSuccessors(current, current.pos);

				for (int i = 0; i < successorCount; i++)
				{
					PathNode next = successors[i];

					if (closedList.Contains(next))
						continue;

					Vec2i nP = next.pos;
					int cost = grid[nP.x, nP.y].cost;
					int newG = current.g + cost;

					PathNode node;
					if (!openList.TryGetValue(next.pos, out node))
					{
						next.g = newG;
						next.h = ComputeHeuristic(nP, target);
						next.f = next.g + next.h;
						next.parent = current;
						openList.Add(next.pos, next);
					}
					else
					{
						if (newG < node.g)
						{
							openList.Remove(node);
							node.g = newG;
							node.f = node.g + node.h;
							node.parent = current;
							openList.Add(node);
						}
					}
				}
			}
		}
		catch (Exception e)
		{
			Debug.LogError("An exception occurred while computing the path: " + e.ToString());
			Debug.LogError(e.StackTrace);
		}
	}

	public void Clear()
	{
		openList.Clear();
		closedList.Clear();
	}
}
