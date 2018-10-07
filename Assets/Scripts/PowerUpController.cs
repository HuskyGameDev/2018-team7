using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    private PlayerController playerController;
    private move playerMovement;
	// Use this for initialization
	void Start () {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Cannot find Player");
        }


        GameObject moveController = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerMovement = moveController.GetComponent<move>();
        }
        else
        {
            Debug.Log("Cannot find movement");
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.name);
        if (collision.name == "Health")
        {
            HealthPickup();
        }
        if (collision.name == "Speed")
        {
            StartCoroutine(SpeedPickup());
        }
        if (collision.name == "BulletSpeed")
        {
            StartCoroutine(ShootingSpeedPickUp());
        }
        Destroy(collision.gameObject);
        
    }

    void HealthPickup()
    {
        playerController.setHealth(playerController.getHealth() + 20);
    }

    IEnumerator SpeedPickup()
    {
        playerMovement.setSpeed(playerMovement.getSpeed() * 2);
        yield return StartCoroutine(PickupWait(30));
        playerMovement.setSpeed(playerMovement.getSpeed() / 2);
    }

    IEnumerator ShootingSpeedPickUp()
    {
        playerController.setBulletSpeed(playerController.getBulletSpeed() + .05f);
        yield return StartCoroutine(PickupWait(30));
        playerController.setBulletSpeed(playerController.getBulletSpeed() - .05f);
    }

    IEnumerator PickupWait(int timer)
    {
        yield return new WaitForSeconds(timer);
    }
}
