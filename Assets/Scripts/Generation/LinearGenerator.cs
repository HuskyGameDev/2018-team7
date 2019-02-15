using System.Collections.Generic;
using UnityEngine;

public class LinearGenerator : FloorGenerator
{
	public LinearGenerator(Floor floor, GameObject enemyPrefab) : base(floor, enemyPrefab) { }

	protected override Vec2i GetNextPos(Vec2i current)
	{
		List<Vec2i> possibleRooms = new List<Vec2i>(4)
		{
			current + Vec2i.Directions[Direction.Front],
			current + Vec2i.Directions[Direction.Right]
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
			floor.MaxRoom = roomP;

			BuildRoom(room, i == stairRoom, powerupRoom);

			Vec2i next = GetNextPos(roomP);

			connections.Add(new Connection(roomP, next, roomP.x != next.x));
			roomP = next;
		}

		// Remove the last connection because it doesn't connect to a room (it's what would connect to
		// the next room if we added another).
		connections.RemoveAt(connections.Count - 1);

		AddConnections(connections);
	}
}
