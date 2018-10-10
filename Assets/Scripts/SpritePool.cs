using System.Collections.Generic;
using UnityEngine;

public class SpritePool : MonoBehaviour
{
	[SerializeField] private GameObject spriteObj;

	private Queue<SpriteRenderer> pool = new Queue<SpriteRenderer>();
	private Transform parent;

	private void Awake()
	{
		parent = GetComponent<Floor>().transform;
	}

	/// <summary>
	/// Get a sprite from the sprite pool. If one doesn't exist, it will be instantiated.
	/// Sprites are guaranteed to be enabled.
	/// </summary>
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

	/// <summary>
	/// Return the sprite to the sprite pool, disabling it in the process.
	/// </summary>
	public void Return(SpriteRenderer rend)
	{
		rend.enabled = false;
		pool.Enqueue(rend);
	}
}
