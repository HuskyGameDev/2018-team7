﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klusterfunk : Gun
{
	private int pelletCount = 1;
	private float spreadAngle;
	public Transform BarrelExit;
	private float timeStamp;
	List<Quaternion> pellets;

	protected override void Start()
	{
		fireRate = 0.01f;
		speed = 25.0f;

		// Temp
		BarrelExit = pc.transform;

		pellets = new List<Quaternion>(pelletCount);
		for (int i = 0; i < pelletCount; i++)
		{
			pellets.Add(Quaternion.Euler(Vector3.zero));
		}

		// Use a spread angle of 360.0f to cover all directions around the player.
		spreadAngle = 360.0f;

		bulletsRemaining = 300;
	}

    private void DoFire(Facing facing)
	{
		if (bulletsRemaining > 0)
		{
			pc.ChangeFacing(facing);

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				Bullet p = CreateBullet(BarrelExit);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.SetSpeed(speed);
				bulletsRemaining--;
			}

			ResetTimeToFire();
		}
	}

	public override void Fire(PlayerController pc)
	{
		timeBeforeFire -= Time.deltaTime;

		if (timeBeforeFire < 0.0f)
		{
            if (Input.GetKey(KeyCode.Mouse0))
                DoFire(Facing.Front);
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
