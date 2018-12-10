using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minigun : Gun
{
	public float timeBetweenShots;
	public int pelletCount;
    public float spreadAngle;
    public float pelletFireVel = 1;
    public Transform BarrelExit;

	Quaternion pellet;

    private float timestamp;

	protected override void Init()
	{
		speed = 15.0f;
		timeBetweenShots = 0.05f;
		BarrelExit = gameObject.transform;
		pelletCount = 1;
		spreadAngle = 10.0f;
		pelletFireVel = 400;
		pellet = Quaternion.Euler(Vector3.zero);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Time.time >= timestamp)
		{
			// Up
			if (Input.GetButton("Fire1"))
			{
				timestamp = Time.time + timeBetweenShots;
				pellet = Random.rotation;
				GameObject p = CreateBullet(BarrelExit, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellet, spreadAngle);
				p.GetComponent<Rigidbody>().isKinematic = false;
				p.GetComponent<BulletController>().speedY = speed;
			}

			// Down
			if (Input.GetButton("Fire2"))
			{
				timestamp = Time.time + timeBetweenShots;
				pellet = Random.rotation;
				GameObject p = CreateBullet(BarrelExit, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellet, spreadAngle);
				p.GetComponent<BulletController>().speedY = -speed;
			}

			// Left
			if (Input.GetButton("Fire3"))
			{
				timestamp = Time.time + timeBetweenShots;
				pellet = Random.rotation;
				GameObject p = CreateBullet(BarrelExit, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellet, spreadAngle);
				p.GetComponent<Rigidbody>().isKinematic = false;
				p.GetComponent<BulletController>().speedX = -speed;
			}

			// Right
			if (Input.GetButton("Fire4"))
			{
				timestamp = Time.time + timeBetweenShots;
				pellet = Random.rotation;
				GameObject p = CreateBullet(BarrelExit, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellet, spreadAngle);
				p.GetComponent<Rigidbody>().isKinematic = false;
				p.GetComponent<BulletController>().speedX = speed;
			}
		}
	}
}