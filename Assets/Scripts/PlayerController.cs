using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject bullet; // The bullet object for the player to shoot

    public int health; // the health of the player

    // Returns the health of the player
    public int getHealth()
    {
        return health;
    }

    // Sets the health of the player
    public void setHealth(int health)
    {
        this.health = health;
    }

    public float bulletSpeed; // the speed at which the player shoots

    // Returns the bulletSpeed of the player
    public float getBulletSpeed()
    {
        return this.bulletSpeed;
    }


    // Sets the bulletSpeed of the player
    public void setBulletSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }

    /**
     * Start
     * Initializes the health and bulletSpeed variables
     */ 
    void Start () {
        health = 100;
        bulletSpeed = .2f;
	}
   
    
	//NOT FINAL
	// Update is called once per frame
    /**
     * Update
     * Temporarily decides which direction the player is going to shoot
     */ 
	void Update ()
	{
        //Shoots Right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        { 
            
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = bulletSpeed;
        }
        
        //Shoots Left
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = -bulletSpeed;
          
        }
        //Shoots Up
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = bulletSpeed;
           
        }
        //Shoots Down
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = -bulletSpeed;
            
        }
    }
}
