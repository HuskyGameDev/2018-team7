using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Stores bullet game objects so that they can be reused without instantiating/destroying.
/// </summary>
public class BulletPool
{
	private GameObject bulletPrefab;

	private Queue<Bullet> bulletPool = new Queue<Bullet>();

	/// <summary>
	/// Returns a new bullet from the pool and sets its position to the position given.
	/// The bullet won't collide with the object firing it.
	/// </summary>
	public Bullet CreateBullet(Transform thing, Transform location, float duration)
	{
		Bullet bullet = GetBullet(duration);
		Vector3 bulletPos = location.position;
		bullet.transform.position = bulletPos;
		Physics.IgnoreCollision(bullet.GetComponent<BoxCollider>(), thing.GetComponentInChildren<BoxCollider>());
		return bullet;
	}

	// Returns a new bullet from the pool if one exists. If not, instantiates a new one.
	private Bullet GetBullet(float duration)
	{
		Bullet bullet;

		if (bulletPool.Count > 0)
		{
			bullet = bulletPool.Dequeue();
			bullet.gameObject.SetActive(true);
		}
		else
		{
			if (bulletPrefab == null)
				bulletPrefab = Resources.Load<GameObject>("Bullet");

			bullet = Object.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity).GetComponent<Bullet>();
			bullet.GetComponent<Rigidbody>().isKinematic = false;
			bullet.Pool = this;
		}

		// Starts the coroutine to destroy the bullet after a certain amount of time.
		bullet.OnFired(duration);

		return bullet;
	}

	/// <summary>
	/// Returns the bullet back to the pool, setting it inactive in the process.
	/// </summary>
	public void ReturnBullet(Bullet obj)
	{
		obj.rotate = false;
		obj.bounce = false;
		obj.gameObject.SetActive(false);
		bulletPool.Enqueue(obj);
	}
}
