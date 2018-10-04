using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePool : MonoBehaviour
{
	[SerializeField] private GameObject spriteObj;

	private Queue<SpriteRenderer> pool = new Queue<SpriteRenderer>();
	private Transform parent;

	private void Awake()
	{
		parent = GameObject.FindWithTag("Floor").transform;
	}

	public SpriteRenderer Get()
	{
		SpriteRenderer rend;

		if (pool.Count > 0)
		{
			rend = pool.Dequeue();
			rend.enabled = true;
		}
		else
		{
			rend = Instantiate(spriteObj).GetComponent<SpriteRenderer>();
			rend.transform.SetParent(parent);
		}

		return rend;
	}

	public void Return(SpriteRenderer rend)
	{
		rend.enabled = false;
		pool.Enqueue(rend);
	}
}
