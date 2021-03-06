﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private PlayerController pc; // the player object
    private Move playerMovement; // Object for the move class\

    private Coroutine fireRateRoutine;
    private Coroutine speedRoutine;

    private AudioSource audioSrc;

    /**
     * Start
     * Instantiates the playerController and the playerMovement objects for later use
     */
    void Start ()
	{
		pc = GetComponentInParent<PlayerController>();
		playerMovement = GetComponentInParent<Move>();
        audioSrc = GetComponent<AudioSource>();
    }

    /**
     * OnTriggerEnter2D
     * Checks what 2D object the player has collided with
     * Uses coroutines in order to have the powerups time out in their methods
     */ 
    void OnTriggerEnter(Collider collision)
    {
		if (collision.CompareTag("Pickup"))
		{
			switch (collision.name)
			{
				case "Health":
					HealthPickup();
                    audioSrc.clip = collision.GetComponent<PowerupAudio>().clip;
                    audioSrc.Play();
					break;

				case "Speed":
                    if (speedRoutine != null)
                        StopCoroutine(speedRoutine);

					speedRoutine = StartCoroutine(SpeedPickup());
                    audioSrc.clip = collision.GetComponent<PowerupAudio>().clip;
                    audioSrc.Play();
                    break;

				case "BulletSpeed":
                    if (fireRateRoutine != null)
                        StopCoroutine(fireRateRoutine);

					fireRateRoutine = StartCoroutine(ShootingSpeedPickUp());
                    audioSrc.clip = collision.GetComponent<PowerupAudio>().clip;
                    audioSrc.Play();
                    break;

				case "Sniper":
					pc.AddGun(GunType.Sniper);
					break;

				case "SMG":
					pc.AddGun(GunType.SMG);
					break;

				case "Minigun":
					pc.AddGun(GunType.Minigun);
					break;

				case "Shotgun":
					pc.AddGun(GunType.Shotgun);
					break;

				case "Klusterfunk":
					pc.AddGun(GunType.Klusterfunk);
					break;

				case "Laser":
					pc.AddGun(GunType.Laser);
					break;
			}

			Destroy(collision.gameObject);
		}
    }

    /**
     * HealthPickup
     * Adds 20 health to the player
     */ 
    void HealthPickup()
    {
		if (pc.health < 100)
			pc.Heal(45);
    }

    /**
     * SpeedPickUp
     * doubles the speed of the player 
     * IEnumerator -> Uses a coroutine in order to wait for 30 seconds
     */ 
    IEnumerator SpeedPickup()
    {
		playerMovement.SpeedModifier = 2.0f;
        yield return StartCoroutine(PickupWait(15));
		playerMovement.SpeedModifier = 1.0f;
    }

    /**
     * ShootingSpeed
     * increases the fire rate of the player.
     * IEnumerator -> Uses a coroutine in order to wait for 30 seconds
     */
    IEnumerator ShootingSpeedPickUp()
    {
		pc.SetFireRateModifier(0.75f);
        yield return StartCoroutine(PickupWait(30));
        pc.SetFireRateModifier(1.0f);
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
