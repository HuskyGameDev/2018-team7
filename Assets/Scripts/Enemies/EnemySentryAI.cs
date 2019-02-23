using UnityEngine;

public class EnemySentryAI : MonoBehaviour
{
	private PlayerController pc;
	private Transform target;
	private float range = 20f;
	private BulletPool bullets = new BulletPool();
	private float timeBetweenShots = 1.0f;
	private float timeStamp;
	private float bulletSpeed = 15.0f;

	private void Start()
	{
		pc = GetComponent<Enemy>().pc;
		target = pc.transform;
	}

	private void Update()
	{
		// If player is in range, shoot in direction
		if (Vector3.Distance(transform.position, target.position) <= range)
			Shoot(transform, target);
	}

	private void Shoot(Transform transform, Transform target)
	{
		timeStamp -= Time.deltaTime;

		if (timeStamp <= 0.0f)
		{
			timeStamp = timeBetweenShots;
			Bullet bullet = bullets.CreateBullet(transform);
			Vector3 dir = (target.position - transform.position).normalized;
			bullet.transform.rotation = Utils.LookX(dir);
			bullet.SetSpeed(bulletSpeed);
			bullet.gameObject.layer = 15;
			bullet.OnFired();
		}
	}
}
