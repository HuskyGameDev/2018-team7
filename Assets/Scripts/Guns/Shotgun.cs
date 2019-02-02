using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
	private int pelletCount = 10;
	private float spreadAngle;
    public Transform BarrelExit;

	protected override void Init()
	{
		speed = 25.0f;
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Shotgun");
	}

	List<Quaternion> pellets;

    void Awake()
	{
		// Temp
		BarrelExit = transform;

        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }

		spreadAngle = 45.0f;
    }

	public override void CheckFire()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			pc.ChangeFacing(Facing.Back);
			audioSource.Play();

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				GameObject p = CreateBullet(BarrelExit, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<BulletController>().speedY = speed;
			}
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			pc.ChangeFacing(Facing.Front);
			audioSource.Play();

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				GameObject p = CreateBullet(BarrelExit, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<BulletController>().speedY = -speed;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			pc.ChangeFacing(Facing.Left);
			audioSource.Play();

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				GameObject p = CreateBullet(BarrelExit, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<BulletController>().speedX = -speed;
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			pc.ChangeFacing(Facing.Right);
			audioSource.Play();

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				GameObject p = CreateBullet(BarrelExit, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<BulletController>().speedX = speed;
			}
		}

	}
}
