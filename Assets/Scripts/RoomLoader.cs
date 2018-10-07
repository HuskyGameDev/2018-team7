
using UnityEngine;
using static Utils;
using static UnityEngine.Mathf;
using System.Collections.Generic;

public class RoomLoader : MonoBehaviour
{
	[SerializeField] private Transform player;

	private Floor floor;
	private bool free = false;

	private Camera cam;

	private const int MaxActive = 4;

	private LinkedList<Room> activeRooms = new LinkedList<Room>();

	private void Awake()
	{
		cam = GetComponent<Camera>();
		floor = GameObject.FindWithTag("Floor").GetComponent<Floor>();
	}

	// Returns a rectangle representing all rooms that intersect the camera's view frustum.
	// The minimum value is the room coordinates of the bottom-left room and the maximum
	// value is the room coordinates of the upper-right room.
	public RectInt GetIntersectingRooms(Floor floor)
	{
		if (free)
		{
			Bounds visible = new Bounds();
			visible.min = cam.ScreenToWorldPoint(Vector3.zero);
			visible.max = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

			visible.Expand(free ? 1.0f : -1.0f);

			Vec2i min = ToRoomPos(visible.min);
			Vec2i max = ToRoomPos(visible.max);

			RectInt bounds = new RectInt();
			bounds.min = new Vector2Int(min.x, min.y);
			bounds.max = new Vector2Int(max.x, max.y);

			return bounds;
		}
		else
		{
			Vec2i pRoom = ToRoomPos(player.position);
			return new RectInt(pRoom.x, pRoom.y, 0, 0);
		}
	}

	// Loads whatever room the camera is within. Only one room is loaded at a time.
	// Assumes the camera is fixed to a room and shows only that room.
	// We could also do a follow camera if we wanted.
	private void Update()
	{
		// Testing code.
		/*if (Input.GetKeyDown(KeyCode.F))
			free = !free;

		// Temporary camera movement code for testing purposes.
		Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		move *= (10.0f * Time.deltaTime);
		transform.Translate(move);*/

		RectInt rooms = GetIntersectingRooms(floor);

		for (int y = rooms.yMin; y <= rooms.yMax; y++)
		{
			for (int x = rooms.xMin; x <= rooms.xMax; x++)
			{
				Room room = floor.GetRoom(new Vec2i(x, y));
				if (room == null) continue;

				if (!room.hasSprites)
				{
					room.SetSprites();
					room.SetColliders();

					if (activeRooms.Contains(room))
						activeRooms.Remove(room);

					activeRooms.AddFirst(room);

					if (activeRooms.Count > MaxActive)
					{
						activeRooms.Last.Value.RemoveSprites();
						activeRooms.Last.Value.RemoveColliders();
						activeRooms.RemoveLast();
					}
				}
			}
		}

		if (!free)
		{
			Room room = floor.GetRoom(ToRoomPos(player.position));

			if (room != null)
			{
				Vec2i wPos = room.Pos * new Vec2i(Room.Width, Room.Height);
				transform.position = new Vector3(wPos.x + Room.HalfSizeX - 0.5f, wPos.y + Room.HalfSizeY - 0.5f, transform.position.z);
			}
		}
	}
}
