﻿using UnityEngine;

public class EnemySentryAI : MonoBehaviour
{
	private PlayerController pc;
	private Transform target;
	private float range = 20f;
	private BulletPool bullets = new BulletPool();

	[SerializeField] private float baseFireRate;
	[SerializeField] private float baseBulletSpeed;

	private float timeBetweenShots = 1.5f;
	private float timeStamp;
	private float bulletSpeed = 10.0f;

	private float startDelay = 0.5f;

	private void Start()
	{
		int floor = Floor.Instance.FloorID;

		// Set bullet speed and time between shots based on the floor we're on.
		// Bullet speed starts at 10 and goes up 1.5 per floor, up to a max of 50.
		// Time between shots starts at 1.5 seconds and falls to a minimum of 0.2 seconds.
		bulletSpeed = Mathf.Min(baseBulletSpeed + ((floor - 1) * 1.5f), 50.0f);
		timeBetweenShots = Mathf.Max(baseFireRate - (((floor - 1) / 2) * 0.15f), 0.2f);

		pc = GetComponent<Enemy>().pc;
		target = pc.transform;
	}

	private void Update()
	{
		startDelay -= Time.deltaTime;

		if (startDelay < 0.0f)
		{
			// If player is in range, shoot in direction
			if (Vector3.Distance(transform.position, target.position) <= range)
				Shoot(transform, target);
		}
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
