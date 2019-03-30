using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : MonoBehaviour
{
	private BulletPool bullets = new BulletPool();

	List<Quaternion> pellets;
	private float spreadAngle = 15.0f;

	[SerializeField] private float baseFireRate;
	[SerializeField] private float baseBulletSpeed;

	private float timeBetweenShots = 1.5f;
	private float timeStamp;
	private float bulletSpeed = 10.0f;

	private PlayerController pc;
	private Transform target;
	private float range = 20f;

	private float startDelay = 0.5f;

	private void Awake()
	{
		int floorID = Floor.Instance.FloorID;

		// Set bullet speed and time between shots based on the floor we're on.
		// Bullet speed starts at 10 and goes up 1.5 per floor, up to a max of 50.
		// Time between shots starts at 1.5 seconds and falls to a minimum of 0.2 seconds.
		bulletSpeed = Mathf.Min(baseBulletSpeed + ((floorID - 1) * 1.5f), 50.0f);
		timeBetweenShots = Mathf.Max(baseFireRate - (((floorID - 1) / 2) * 0.15f), 0.2f);

		pc = GetComponent<Enemy>().pc;
		target = pc.transform;

		pellets = new List<Quaternion>();

		for (int i = 0; i < 10; i++)
			pellets.Add(Quaternion.Euler(Vector3.zero));
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

	public void Shoot(Transform transform, Transform target)
	{
		timeStamp -= Time.deltaTime;

		if (timeStamp <= 0.0f)
		{
			for (int i = 9; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				Bullet p = bullets.CreateBullet(transform);
				Vector3 dir = (target.position - transform.position).normalized;
				p.transform.rotation = Utils.LookX(dir);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.SetSpeed(bulletSpeed);
				p.gameObject.layer = 15;
				p.OnFired();
			}

			timeStamp = timeBetweenShots;
		}
	}
}
