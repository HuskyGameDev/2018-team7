using UnityEngine;

public class Pistol : Gun
{
	/**
	* Update
	* Temporarily decides which direction the player is going to shoot
	*/
	void Update()
	{
		//Shoots Right
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{

			GameObject go = (GameObject)Instantiate(bullet,
			transform.position, Quaternion.identity);
			go.GetComponent<BulletController>().speedX = speed;
		}

		//Shoots Left
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			GameObject go = (GameObject)Instantiate(bullet,
			transform.position, Quaternion.identity);
			go.GetComponent<BulletController>().speedX = -speed;

		}
		//Shoots Up
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{

			GameObject go = (GameObject)Instantiate(bullet,
			transform.position, Quaternion.identity);
			go.GetComponent<BulletController>().speedY = speed;

		}
		//Shoots Down
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{

			GameObject go = (GameObject)Instantiate(bullet,
			transform.position, Quaternion.identity);
			go.GetComponent<BulletController>().speedY = -speed;

		}
	}
}
