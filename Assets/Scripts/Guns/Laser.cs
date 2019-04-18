using UnityEngine;

public class Laser : Gun
{
	public float spreadAngle;
	public Transform BarrelExit;

	protected override void Start()
	{
		fireRate = 0.01f;
		speed = 12.0f;
		damage = 1;
		bulletsRemaining = 400;
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
		ResetTimeToFire();
		bulletsRemaining--;
	}

    private void DoFire(Vector3 aimPos) {
        // If out of bullets, don't fire
        if (bulletsRemaining <= 0)
            return;
        Quaternion rot = Random.rotation;
        aimPos = aimPos - pc.transform.position;
        Bullet p = CreateBullet(pc.transform);
        p.ChangeFacing(aimPos);
        p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, rot, spreadAngle);
        p.SetSpeed(speed);
        ResetTimeToFire();
        bulletsRemaining--;
    }

    public override void Fire(PlayerController pc)
	{
		timeBeforeFire -= Time.deltaTime;

		if (timeBeforeFire < 0.0f && bulletsRemaining > 0)
		{
            if (Input.GetKey(KeyCode.Mouse0))
                DoFire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
