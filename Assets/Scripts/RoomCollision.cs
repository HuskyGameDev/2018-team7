using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class RoomCollision
{
	private Room room;
	private ColliderPool pool;
	private bool hasColliders;

	private List<TileCollider> colliders = new List<TileCollider>();

	public RoomCollision(Room room, ColliderPool pool)
	{
		this.room = room;
		this.pool = pool;
	}

	/// <summary>
	/// Scans the tiles in the room and uses their tile properties objects to
	/// determine if they should have a collider or not. If so, adds one and
	/// sets its properties according to the tile's requirement.
	/// </summary>
	public void Generate()
	{
		Assert.IsFalse(hasColliders);

		Vec2i worldPos = room.Pos * new Vec2i(Room.Width, Room.Height);

		for (int y = 0; y < Room.Height; y++)
		{
			for (int x = 0; x < Room.Width; x++)
			{
				Tile tile = room.GetTile(x, y);
				TileProperties props = tile.Properties;

				if (props.hasCollider)
				{
					TileCollider col = pool.Get();
					col.SetPosition(new Vector2(worldPos.x + x, worldPos.y + y) + props.colliderOffset);
					col.SetSize(props.colliderSize);
					col.SetTile(tile);
					col.SetTrigger(props.trigger);
					col.gameObject.layer = props.trigger ? 14 : 13;
					colliders.Add(col);
				}
			}
		}

		hasColliders = true;
	}

	/// <summary>
	/// Remove all colliders from the room.
	/// </summary>
	public void RemoveColliders()
	{
		if (hasColliders)
		{
			pool.ReturnColliders(colliders);
			hasColliders = false;
		}
		else
		{
			Debug.LogWarning("Tried to remove the colliders from a room that doesn't have any.");
			Debug.LogWarning("Room: " + room.Pos);
		}
	}
}
