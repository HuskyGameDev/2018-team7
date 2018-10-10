using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		TileCollider tC = col.GetComponent<TileCollider>();

		if (tC != null)
		{
			switch (tC.tile.id)
			{
				// Generate a new floor when colliding with stairs.
				case TileType.Stair:
					Floor.Instance.Generate();
					break;
			}
		}
	}
}
