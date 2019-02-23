using UnityEngine;
using System.Collections.Generic;

public class BulletPool
{
	private GameObject bulletPrefab;

	private Queue<Bullet> bulletPool = new Queue<Bullet>();

	public Bullet CreateBullet(Transform t)
	{
		Bullet bullet = GetBullet();
		bullet.transform.position = t.position;
		Physics.IgnoreCollision(bullet.GetComponent<BoxCollider>(), t.GetComponentInChildren<BoxCollider>());
		return bullet;
	}

	private Bullet GetBullet()
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

		bullet.OnFired();
		return bullet;
	}

	public void ReturnBullet(Bullet obj)
	{
		obj.gameObject.SetActive(false);
		bulletPool.Enqueue(obj);
	}
}
