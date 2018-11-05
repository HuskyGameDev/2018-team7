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
		speed = 0.4f;
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

    // Update is called once per frame
    void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			audioSource.Play();

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				GameObject p = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<BulletController>().speedY = speed;
			}
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			audioSource.Play();

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				GameObject p = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<BulletController>().speedY = -speed;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			audioSource.Play();

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				GameObject p = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<BulletController>().speedX = -speed;
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			audioSource.Play();

			for (int i = pellets.Count - 1; i >= 0; i--)
			{
				pellets[i] = Random.rotation;
				GameObject p = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<BulletController>().speedX = speed;
			}
		}

	}
}
