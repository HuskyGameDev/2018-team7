using UnityEngine;

public class Sniper : Gun
{
	public float timeBetweenShots = 1.0f;

	private float timestamp;

	protected override void Start()
	{
		speed = 18.0f;
	}

	public override void Activate(PlayerController pc)
	{
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Sniper Rifle");
	}

	public override void Fire(PlayerController pc)
	{
		//Shoots Right
		if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.RightArrow))
		{
			pc.ChangeFacing(Facing.Right);
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			Bullet go = CreateBullet(pc.transform);
			go.SetSpeed(speed);
		}

		//Shoots Left
		else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.LeftArrow))
		{
			pc.ChangeFacing(Facing.Left);
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			Bullet go = CreateBullet(pc.transform);
			go.SetSpeed(speed);

		}
		//Shoots Up
		else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.UpArrow))
		{
			pc.ChangeFacing(Facing.Back);
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			Bullet go = CreateBullet(pc.transform);
			go.SetSpeed(speed);

		}
		//Shoots Down
		else if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.DownArrow))
		{
			pc.ChangeFacing(Facing.Front);
			audioSource.Play();

			//Instantiate(bullet, transform.position, transform.rotation);
			timestamp = Time.time + timeBetweenShots;

			Bullet go = CreateBullet(pc.transform);
			go.SetSpeed(speed);

		}
	}
}
