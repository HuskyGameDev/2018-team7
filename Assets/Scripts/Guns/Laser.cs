using UnityEngine;

public class Laser : Gun
{
	public float spreadAngle;
	public Transform BarrelExit;

	// tracks bullet count
	//public int bulletsRemaining = 400;

	protected override void Start()
	{
		fireRate = 0.01f;
		speed = 12.0f;
		damage = 1;
	}

	private void DoFire(Facing facing)
	{
		// If out of bullets, don't fire
		if (bulletsRemaining <= 0)
			return;
		pc.ChangeFacing(facing);
		Quaternion rot = Random.rotation;
		Bullet p = CreateBullet(pc.transform);
		p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, rot, spreadAngle);
		p.SetSpeed(speed);
		timeBeforeFire = fireRate;
		bulletsRemaining--;
	}

	public override void Fire(PlayerController pc)
	{
		timeBeforeFire -= Time.deltaTime;

		if (timeBeforeFire < 0.0f && bulletsRemaining > 0)
		{
			if (Input.GetKey(KeyCode.RightArrow))
				DoFire(Facing.Right);
			else if (Input.GetKey(KeyCode.LeftArrow))
				DoFire(Facing.Left);
			else if (Input.GetKey(KeyCode.UpArrow))
				DoFire(Facing.Back);
			else if (Input.GetKey(KeyCode.DownArrow))
				DoFire(Facing.Front);
		}
	}
}
