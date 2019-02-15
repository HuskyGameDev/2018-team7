using System.Collections.Generic;
using UnityEngine;

public class LinearGenerator : FloorGenerator
{
	private bool forceStairs = false;

	public LinearGenerator(Floor floor, GameObject enemyPrefab) : base(floor, enemyPrefab) { }

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

	public override void Generate(int roomCount)
	{
		// Room position.
		Vec2i roomP = new Vec2i(0, 0);

		// Used to pair two rooms together. These two rooms are connected and will have a path between them.
		List<Connection> connections = new List<Connection>(roomCount);
		int stairRoom = Random.Range(roomCount / 2, roomCount);

		for (int i = 0; i < roomCount; i++)
		{
			bool powerupRoom = Random.value < 0.6f;

			Room room = floor.CreateRoom(roomP.x, roomP.y);

			if (roomP.x > floor.MaxRoom.x)
				floor.MaxRoom = new Vec2i(roomP.x, floor.MaxRoom.y);

			if (roomP.y > floor.MaxRoom.y)
				floor.MaxRoom = new Vec2i(floor.MaxRoom.x, roomP.y);

			BuildRoom(room, forceStairs ? true : i == stairRoom, powerupRoom);

			Vec2i next;
			int attempts = 0;

			// Get the next room position. We'll keep trying to get a new position if
			// the position is negative or if the room already exists.
			do
			{
				next = GetNextPos(roomP);
				
				if (++attempts == 16)
				{
					forceStairs = true;
					break;
				}
			}
			while (next.x < 0 || next.y < 0 || floor.GetRoom(next) != null);

			connections.Add(new Connection(roomP, next, roomP.x != next.x));
			roomP = next;
		}

		// Remove the last connection because it doesn't connect to a room (it's what would connect to
		// the next room if we added another).
		connections.RemoveAt(connections.Count - 1);

		AddConnections(connections);
	}
}
