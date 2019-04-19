using UnityEngine;

public class Minigun : Gun
{
    public int pelletCount;
    public float spreadAngle;
    public float pelletFireVel = 1;
    public Transform BarrelExit;

	Quaternion pellet;

	protected override void Start()
	{
		speed = 15.0f;
		fireRate = 0.05f;
		BarrelExit = pc.transform;
		pelletCount = 1;
		spreadAngle = 10.0f;
		pelletFireVel = 400;
		pellet = Quaternion.Euler(Vector3.zero);
        bulletsRemaining = 100;
	}

    public override void Activate(PlayerController pc)
    {
        audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Assault");
    }

    private void DoFire(Facing facing)
	{
        // If out of bullets, don't fire
        if (bulletsRemaining <= 0)
            return;

		pc.ChangeFacing(facing);
		pellet = Random.rotation;
        audioSource.Play();
        Bullet p = CreateBullet(BarrelExit);
		p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellet, spreadAngle);
		p.SetSpeed(speed);
		ResetTimeToFire();
		bulletsRemaining--;
    }

    private void DoFire(Vector3 aimPos) {
        // If out of bullets, don't fire
        if (bulletsRemaining <= 0)
            return;

        audioSource.Play();
        pellet = Random.rotation;
        aimPos = aimPos - pc.transform.position;
        Bullet p = CreateBullet(BarrelExit);
        p.ChangeFacing(aimPos);
        p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellet, spreadAngle);
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
            else if (Input.GetKey(KeyCode.RightArrow))
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