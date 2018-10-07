using System.Collections.Generic;
using UnityEngine;

public class ColliderPool : MonoBehaviour
{
	[SerializeField] private GameObject colliderObj;

	private Queue<TileCollider> pool = new Queue<TileCollider>();
	private Transform parent;

	public TileCollider Get()
	{
		TileCollider col;

		if (pool.Count > 0)
		{
			col = pool.Dequeue();
			col.enabled = true;
		}
		else
		{
			col = Instantiate(colliderObj).GetComponent<TileCollider>();
			col.transform.SetParent(transform);
		}

		return col;
	}

	public void ReturnColliders(List<TileCollider> colliders)
	{
		for (int i = 0; i < colliders.Count; i++)
		{
			colliders[i].enabled = false;
			pool.Enqueue(colliders[i]);
		}
	}
}
