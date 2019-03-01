using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	private void OnTriggerEnter(Collider col)
	{
		TileCollider tC = col.GetComponent<TileCollider>();

		if (tC != null)
		{
			switch (tC.tile.id)
			{
				// Generate a new floor when colliding with stairs.
				case TileType.Stair:
					Floor floor = Floor.Instance;
					floor.Destroy();
					floor.Generate(floor.FloorID + 1);
					break;
			}
		}
	}
}
