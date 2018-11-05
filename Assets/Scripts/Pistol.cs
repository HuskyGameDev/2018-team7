using System.Collections;
using UnityEngine;

public class Pistol : Gun
{

    public AudioClip gunClip;
    public AudioSource audioSource;
    GameObject player;

    private new void Start()
    {
		base.Start();
		player = GameObject.FindWithTag("Player");
        audioSource = player.GetComponent<AudioSource>(); //Attempt to get the AudioSource off of the player
        Debug.Log(player.GetComponent<AudioSource>().name); //Print out if you actually got it
        audioSource.clip = gunClip; //Set the sound for the pistol
        audioSource.Play();
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
            GameObject go = (GameObject)Instantiate(bullet,
			transform.position, Quaternion.identity);
			go.GetComponent<BulletController>().speedX = speed;
		}

		//Shoots Left
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
            audioSource.Play(); //Play gunshot 
            GameObject go = (GameObject)Instantiate(bullet,
			transform.position, Quaternion.identity);
			go.GetComponent<BulletController>().speedX = -speed;

		}
		//Shoots Up
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
            audioSource.Play(); //Play gunshot 
            GameObject go = (GameObject)Instantiate(bullet,
			transform.position, Quaternion.identity);
			go.GetComponent<BulletController>().speedY = speed;

		}
		//Shoots Down
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
            audioSource.Play(); //Play gunshot 
            GameObject go = (GameObject)Instantiate(bullet,
			transform.position, Quaternion.identity);
			go.GetComponent<BulletController>().speedY = -speed;

		}
	}

    

}
