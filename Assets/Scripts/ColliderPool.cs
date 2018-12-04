using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Maintains a pool of collider objects to be used by the game for tiles.
/// </summary>
public class ColliderPool : MonoBehaviour
{
	[SerializeField] private GameObject colliderObj;

	private Queue<TileCollider> pool = new Queue<TileCollider>();
	private Transform parent;

	/// <summary>
	/// Get a collider from the pool. If one doesn't exist, a new one will be instantiated. 
	/// The returned collider is guaranteed to be enabled.
	/// </summary>
	public TileCollider Get()
	{
		TileCollider col;

		if (pool.Count > 0)
		{
			col = pool.Dequeue();

			// HACK: Temporary fix. Something is making colliders in the queue become null.
			// We should fix this properly.
			if (col == null)
				return Get();

			col.Enable();
		}
		else
		{
			col = Instantiate(colliderObj).GetComponent<TileCollider>();
			col.transform.SetParent(transform);
		}

		return col;
	}

	/// <summary>
	/// Returns all colliders in the given list back to the pool, disabling them in the process.
	/// </summary>
	public void ReturnColliders(List<TileCollider> colliders)
	{
		for (int i = 0; i < colliders.Count; i++)
		{
			colliders[i].Disable();
			pool.Enqueue(colliders[i]);
		}

		colliders.Clear();
	}
}
