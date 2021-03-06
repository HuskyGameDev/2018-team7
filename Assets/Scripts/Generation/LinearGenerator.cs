﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates rooms linearly - that is, there's one path from the first room to the last room.
/// It can generate rooms in any of the four directions.
/// </summary>
public class LinearGenerator : FloorGenerator
{
	public LinearGenerator(Floor floor, GameObject[] enemyPrefabs) : base(floor, enemyPrefabs) { }

	// Returns the position the next room will generate in.
	protected override Vec2i GetNextPos(Vec2i current)
	{
		List<Vec2i> possibleRooms = new List<Vec2i>(4)
		{
			current + Vec2i.Directions[Direction.Front],
			current + Vec2i.Directions[Direction.Right],
			current + Vec2i.Directions[Direction.Left],
			current + Vec2i.Directions[Direction.Back]
		};

		return possibleRooms[Random.Range(0, possibleRooms.Count)];
	}

	/// <summary>
	/// Creates all the rooms making up the floor.
	/// </summary>
	public override void Generate()
	{
		int roomCount = Random.Range(8, 13);

		// Room position.
		Vec2i roomP = new Vec2i(0, 0);

		// Used to pair two rooms together. These two rooms are connected and will have a path between them.
		List<Connection> connections = new List<Connection>(roomCount);

		for (int i = 0; i < roomCount; i++)
		{
			Vec2i next;
			int attempts = 0;

			// Get the next room position. We'll keep trying to get a new position if
			// the position is negative or if the room already exists.
			do
			{
				next = GetNextPos(roomP);

				if (++attempts == 16)
				{
					next = Vec2i.NegOne;
					break;
				}
			}
			while (next.x < 0 || next.y < 0 || floor.GetRoom(next) != null);

			connections.Add(new Connection(roomP, next, roomP.x != next.x));

			bool powerupRoom = Random.value < 0.6f;

			Room room = floor.CreateRoom(roomP.x, roomP.y);

			if (roomP.x > floor.MaxRoom.x)
				floor.MaxRoom = new Vec2i(roomP.x, floor.MaxRoom.y);

			if (roomP.y > floor.MaxRoom.y)
				floor.MaxRoom = new Vec2i(floor.MaxRoom.x, roomP.y);

			if (next == Vec2i.NegOne)
			{
				BuildRoom(room, true, powerupRoom);
				break;
			}
			else BuildRoom(room, i == roomCount - 1, powerupRoom);

			roomP = next;
		}

		// Remove the last connection because it doesn't connect to a room (it's what would connect to
		// the next room if we added another).
		connections.RemoveAt(connections.Count - 1);

		AddConnections(connections);
	}
}
