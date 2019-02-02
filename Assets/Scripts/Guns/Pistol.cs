using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class Pistol : Gun
{
	protected override void Init()
	{
		base.Init();
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Handgun");
		Assert.IsNotNull(audioSource.clip);
	}

	public override void CheckFire()
	{
		//Shoots Right
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			pc.ChangeFacing(Facing.Right);
			audioSource.Play(); //Play gunshot 
			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedX = speed;
		}

		//Shoots Left
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			pc.ChangeFacing(Facing.Left);
			audioSource.Play(); //Play gunshot 
			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedX = -speed;

		}
		//Shoots Up
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			pc.ChangeFacing(Facing.Back);
			audioSource.Play(); //Play gunshot 
			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedY = speed;

		}
		//Shoots Down
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			pc.ChangeFacing(Facing.Front);
			audioSource.Play(); //Play gunshot 
			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedY = -speed;

		}
	}
}
