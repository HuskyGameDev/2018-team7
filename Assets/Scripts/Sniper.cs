using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Gun
{
    public float timeBetweenShots = 1.0f;

    private float timestamp;

	protected override void Init()
	{
		speed = 0.1f;
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Sniper Rifle");
	}

	// Update is called once per frame
	void Update()
    {
        //Shoots Right
        if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.RightArrow))
        {
			audioSource.Play();

           //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;

			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedX = speed;
        }

        //Shoots Left
        else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.LeftArrow))
        {
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedX = -speed;

        }
        //Shoots Up
        else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.UpArrow))
        {
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedY = speed;

        }
        //Shoots Down
        else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.DownArrow))
        {
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedY = -speed;

        }

    }
}
