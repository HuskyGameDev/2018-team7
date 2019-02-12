using UnityEngine;

public class Pistol : Gun
{
	public override void Fire(PlayerController pc)
	{
		//Shoots Right
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			pc.ChangeFacing(Facing.Right);
			audioSource.Play(); //Play gunshot 
			BulletController go = CreateBullet(pc.transform);
			go.SetVelocity(Vector3.right * speed);
		}

		//Shoots Left
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			pc.ChangeFacing(Facing.Left);
			audioSource.Play(); //Play gunshot 
			BulletController go = CreateBullet(pc.transform);
			go.SetVelocity(Vector3.left * speed);

		}
		//Shoots Up
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			pc.ChangeFacing(Facing.Back);
			audioSource.Play(); //Play gunshot 
			BulletController go = CreateBullet(pc.transform);
			go.SetVelocity(Vector3.up * speed);

		}
		//Shoots Down
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			pc.ChangeFacing(Facing.Front);
			audioSource.Play(); //Play gunshot 
			BulletController go = CreateBullet(pc.transform);
			go.SetVelocity(Vector3.down * speed);
		}
	}
}
