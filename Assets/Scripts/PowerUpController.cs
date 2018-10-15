using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    private PlayerController playerController; // the player object

    private Move playerMovement; // Object for the move class
	
    /**
     * Start
     * Instantiates the playerController and the playerMovement objects for later use
     */
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
            playerMovement = moveController.GetComponent<Move>();
        }
        else
        {
            Debug.Log("Cannot find movement");
        }
    }

    /**
     * OnTriggerEnter2D
     * Checks what 2D object the player has collided with
     * Uses coroutines in order to have the powerups time out in their methods
     */ 
    void OnTriggerEnter(Collider collision)
    {

        Debug.Log(collision.name);

        // If collision is with Health powerup, call healthPickup
        if (collision.name == "Health")
        {
            HealthPickup();
        }

        // If collison is with Speed powerup, call SpeedPickup
        if (collision.name == "Speed")
        {
            StartCoroutine(SpeedPickup());
        }

        // If collision is with BulletSpeed powerup, call ShootingSpeedPickup
        if (collision.name == "BulletSpeed")
        {
            StartCoroutine(ShootingSpeedPickUp());
        }
        Destroy(collision.gameObject);
        
    }

    /**
     * HealthPickup
     * Adds 20 health to the player
     */ 
    void HealthPickup()
    {
        playerController.setHealth(playerController.getHealth() + 20);
    }

    /**
     * SpeedPickUp
     * doubles the speed of the player 
     * IEnumerator -> Uses a coroutine in order to wait for 30 seconds
     */ 
    IEnumerator SpeedPickup()
    {
        playerMovement.setSpeed(playerMovement.getSpeed() * 2);
        yield return StartCoroutine(PickupWait(30));
        playerMovement.setSpeed(playerMovement.getSpeed() / 2);
    }

    /**
     * ShootingSpeed
     * increases the shooting speed of the player by .05f 
     * IEnumerator -> Uses a coroutine in order to wait for 30 seconds
     */
    IEnumerator ShootingSpeedPickUp()
    {
        playerController.setBulletSpeed(playerController.getBulletSpeed() + .05f);
        yield return StartCoroutine(PickupWait(30));
        playerController.setBulletSpeed(playerController.getBulletSpeed() - .05f);
    }

    /**
     * PickupWait
     * Waits for timer seconds
     * input: timer -> the amount of seconds to be waited
     * output: IEnumerator -> requires a coroutine to wait the specified time
     */ 
    IEnumerator PickupWait(int timer)
    {
        yield return new WaitForSeconds(timer);
    }
}
