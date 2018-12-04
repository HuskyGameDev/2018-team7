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

	/**
	* Update
	* Temporarily decides which direction the player is going to shoot
	*/
	void Update()
	{
		//Shoots Right
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
            audioSource.Play(); //Play gunshot 
			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedX = speed;
		}

		//Shoots Left
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
            audioSource.Play(); //Play gunshot 
			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedX = -speed;

		}
		//Shoots Up
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
            audioSource.Play(); //Play gunshot 
			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedY = speed;

		}
		//Shoots Down
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
            audioSource.Play(); //Play gunshot 
			GameObject go = CreateBullet(transform);
			go.GetComponent<BulletController>().speedY = -speed;

		}
	}

    

}
