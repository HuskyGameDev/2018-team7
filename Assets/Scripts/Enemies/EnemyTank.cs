using UnityEngine;

public class EnemyTank : MonoBehaviour
{
	private BulletPool bullets = new BulletPool();

	private float spreadAngle = 10.0f;

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

		bulletSpeed = Mathf.Min(baseBulletSpeed + ((floorID - 1) * 1.5f), 50.0f);
		timeBetweenShots = Mathf.Max(baseFireRate - (((floorID - 1) / 2) * 0.15f), 0.2f);

		pc = GetComponent<Enemy>().pc;
		target = pc.transform;
	}

	private void Update()
	{
		startDelay -= Time.deltaTime;

		if (startDelay < 0.0f)
		{
			if (Vector3.Distance(transform.position, target.position) <= range)
				Shoot(transform, target);
		}
	}

	public void Shoot(Transform transform, Transform target)
	{
		timeStamp -= Time.deltaTime;

		if (timeStamp <= 0.0f)
		{
			Quaternion rot = Random.rotation;
			Bullet p = bullets.CreateBullet(transform);
			Vector3 dir = (target.position - transform.position).normalized;
			p.transform.rotation = Utils.LookX(dir);
			p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, rot, spreadAngle);
			p.SetSpeed(bulletSpeed);
			p.gameObject.layer = 15;
			p.OnFired();
			timeStamp = timeBetweenShots;
		}
	}
}
