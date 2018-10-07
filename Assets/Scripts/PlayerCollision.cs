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
				case TileType.Stair:
					Floor.Instance.Generate();
					break;
			}
		}
	}
}
