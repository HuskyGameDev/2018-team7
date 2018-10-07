using System.Collections.Generic;
using UnityEngine;

public class ColliderPool : MonoBehaviour
{
	[SerializeField] private GameObject colliderObj;

	private Queue<BoxCollider2D> pool = new Queue<BoxCollider2D>();
	private Transform parent;

	private void Awake()
	{
		parent = GetComponent<Floor>().transform;
	}

	public BoxCollider2D Get()
	{
		BoxCollider2D col;

		if (pool.Count > 0)
		{
			col = pool.Dequeue();
			col.enabled = true;
		}
		else
		{
			col = Instantiate(colliderObj).GetComponent<BoxCollider2D>();
			col.transform.SetParent(parent);
		}

		return col;
	}

	public void ReturnColliders(List<BoxCollider2D> colliders)
	{
		for (int i = 0; i < colliders.Count; i++)
		{
			colliders[i].enabled = false;
			pool.Enqueue(colliders[i]);
		}
	}
}
