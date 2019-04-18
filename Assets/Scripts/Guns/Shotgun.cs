using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
	private int pelletCount = 10;
	private float spreadAngle;
    public Transform BarrelExit;
    private float timeStamp;
	List<Quaternion> pellets;

	protected override void Start()
	{
		fireRate = 0.75f;
		speed = 25.0f;

		// Temp
		BarrelExit = pc.transform;

		pellets = new List<Quaternion>(pelletCount);
		for (int i = 0; i < pelletCount; i++)
		{
			pellets.Add(Quaternion.Euler(Vector3.zero));
		}

		spreadAngle = 15.0f;

		bulletsRemaining = 10;
	}

	public override void Activate(PlayerController pc)
	{
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Shotgun");
	}

	private void DoFire(Facing facing)
	{
        // If out of bullets, don't fire
        if (bulletsRemaining <= 0)
            return;

		pc.ChangeFacing(facing);
		audioSource.Play();

		for (int i = pellets.Count - 1; i >= 0; i--)
		{
			pellets[i] = Random.rotation;
			Bullet p = CreateBullet(BarrelExit);
			p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
			p.SetSpeed(speed);
		}

		ResetTimeToFire();
		bulletsRemaining--;
	}

    private void DoFire(Vector3 aimPos) {
        // If out of bullets, don't fire
        if (bulletsRemaining <= 0)
            return;

        aimPos = aimPos - pc.transform.position;
        audioSource.Play();

        for (int i = pellets.Count - 1; i >= 0; i--) {
            pellets[i] = Random.rotation;
            Bullet p = CreateBullet(BarrelExit);
            p.ChangeFacing(aimPos);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
            p.SetSpeed(speed);
        }

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
            else if (Input.GetKey(KeyCode.UpArrow))
				DoFire(Facing.Back);
			else if (Input.GetKey(KeyCode.DownArrow))
				DoFire(Facing.Front);
			else if (Input.GetKey(KeyCode.LeftArrow))
				DoFire(Facing.Left);
			else if (Input.GetKey(KeyCode.RightArrow))
				DoFire(Facing.Right);
		}
	}
}
