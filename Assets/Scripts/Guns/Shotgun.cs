using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
	private int pelletCount = 10;
	private float spreadAngle;
    public Transform BarrelExit;
    private float timeBetweenShots = 1.0f;
    private float timeStamp;
	List<Quaternion> pellets;

	protected override void Start()
	{
		speed = 25.0f;

		// Temp
		BarrelExit = pc.transform;

		pellets = new List<Quaternion>(pelletCount);
		for (int i = 0; i < pelletCount; i++)
		{
			pellets.Add(Quaternion.Euler(Vector3.zero));
		}

		spreadAngle = 45.0f;
	}

	public override void Activate(PlayerController pc)
	{
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Shotgun");
	}

	public override void Fire(PlayerController pc)
	{
		if (Time.time >= timeStamp && Input.GetKeyDown(KeyCode.UpArrow))
		{
			pc.ChangeFacing(Facing.Back);
			audioSource.Play();
            timeStamp = Time.time + timeBetweenShots;
			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				BulletController p = CreateBullet(BarrelExit);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.SetSpeed(speed);
			}
		}
		else if (Time.time >= timeStamp && Input.GetKeyDown(KeyCode.DownArrow))
		{
			pc.ChangeFacing(Facing.Front);
			audioSource.Play();
            timeStamp = Time.time + timeBetweenShots;
            for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				BulletController p = CreateBullet(BarrelExit);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.SetSpeed(speed);
			}
		}
		else if (Time.time >= timeStamp && Input.GetKeyDown(KeyCode.LeftArrow))
		{
			pc.ChangeFacing(Facing.Left);
			audioSource.Play();
            timeStamp = Time.time + timeBetweenShots;
            for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				BulletController p = CreateBullet(BarrelExit);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.SetSpeed(speed);
			}
		}
		else if (Time.time >= timeStamp && Input.GetKeyDown(KeyCode.RightArrow))
		{
			pc.ChangeFacing(Facing.Right);
			audioSource.Play();
            timeStamp = Time.time + timeBetweenShots;
            for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				BulletController p = CreateBullet(BarrelExit);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.SetSpeed(speed);
			}
		}

	}
}
