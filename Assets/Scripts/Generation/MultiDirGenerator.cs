using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDirGenerator : FloorGenerator
{
	private Vec2i biasedDir;

	public MultiDirGenerator(Floor floor, GameObject enemyPrefab) : base(floor, enemyPrefab)
	{
		biasedDir = Vec2i.Directions[Random.Range(0, Vec2i.Directions.Length)];
	}

	protected override Vec2i GetNextPos(Vec2i current)
	{
		Vec2i biased = current + biasedDir;

		if (Random.value < 0.4f)
			return biased;

		List<Vec2i> possibleRooms = new List<Vec2i>(4)
		{
			current + Vec2i.Directions[Direction.Front],
			current + Vec2i.Directions[Direction.Right],
			current + Vec2i.Directions[Direction.Left],
			current + Vec2i.Directions[Direction.Back]
		};

		possibleRooms.Remove(biased);
		return possibleRooms[Random.Range(0, possibleRooms.Count)];
	}

	public override void Generate(int roomCount)
	{
		roomCount += (roomCount / 2);

		// Room position.
		Vec2i roomP = new Vec2i(0, 0);

		// Used to pair two rooms together. These two rooms are connected and will have a path between them.
		List<Connection> connections = new List<Connection>(roomCount);
		int stairRoom = Random.Range(roomCount / 2, roomCount);

		for (int i = 0; i < roomCount; i++)
		{
			bool powerupRoom = Random.value < 0.6f;

			Room room = floor.CreateRoom(roomP.x, roomP.y);

			if (roomP.LengthSq > floor.MaxRoom.LengthSq)
				floor.MaxRoom = roomP;

			if (roomP.LengthSq < floor.MinRoom.LengthSq)
				floor.MinRoom = roomP;

			BuildRoom(room, i == stairRoom, powerupRoom);

			Vec2i next;

			do
			{
				do
				{
					// Ensure the room position isn't negative.
					next = GetNextPos(roomP);
				}
				while (next.x < 0 || next.y < 0);

				connections.Add(new Connection(roomP, next, roomP.x != next.x));
				roomP = next;
			}
			while (floor.GetRoom(next) != null);
		}

		// Remove the last connection because it doesn't connect to a room (it's what would connect to
		// the next room if we added another).
		connections.RemoveAt(connections.Count - 1);

		AddConnections(connections);
	}
}
