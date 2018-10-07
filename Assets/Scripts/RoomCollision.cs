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
					colliders.Add(col);
				}
			}
		}

		hasColliders = true;
	}

	public void RemoveColliders()
	{
		if (hasColliders)
		{
			pool.ReturnColliders(colliders);
			colliders.Clear();
			hasColliders = false;
		}
	}
}
