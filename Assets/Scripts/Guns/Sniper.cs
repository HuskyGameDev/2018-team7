using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Gun
{
    public float timeBetweenShots = 1.0f;

    private float timestamp;

	protected override void Init()
	{
		speed = 18.0f;
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Sniper Rifle");
	}

	public override void CheckFire()
	{
        //Shoots Right
        if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.RightArrow))
        {
			pc.ChangeFacing(Facing.Right);
			audioSource.Play();

           //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;

			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedX = speed;
        }

        //Shoots Left
        else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.LeftArrow))
        {
			pc.ChangeFacing(Facing.Left);
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedX = -speed;

        }
        //Shoots Up
        else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.UpArrow))
        {
			pc.ChangeFacing(Facing.Back);
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedY = speed;

        }
        //Shoots Down
        else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.DownArrow))
        {
			pc.ChangeFacing(Facing.Front);
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedY = -speed;

        }

    }
}
