using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject bullet;

    public int health;
    public int getHealth()
    {
        return health;
    }

    public void setHealth(int health)
    {
        this.health = health;
    }

    public float bulletSpeed;
    public float getBulletSpeed()
    {
        return this.bulletSpeed;
    }

    public void setBulletSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }

    // Use this for initialization
    void Start () {
        health = 100;
        bulletSpeed = .05f;
	}
   
    
	//NOT FINAL
	// Update is called once per frame
	void Update () {

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
