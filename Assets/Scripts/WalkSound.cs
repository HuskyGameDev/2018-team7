using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Author: Noah de Longpre'
 * Edited by Jason Bricco
 * Generates the footstep sounds for the player character.
 * 
 */
public class WalkSound : MonoBehaviour
{

    CharacterController cc;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public AudioClip currentClip;
    private bool coroutineOn;
    public float stepdelay;

    void Start()
    {
        cc = this.gameObject.GetComponent<CharacterController>(); //Find the character controller so we can see if the player is moving.
        stepdelay = .25f; //Every quarter of a second, play a walk step sound
        coroutineOn = true; //Let the co-routine know that the script has started.

        audioSource.clip = audioClips[0]; //Set the base sound
        StartCoroutine(Walking()); //Start-er up

    }

    // Update is called once per frame
    void Update()
    {
        //Makes sure the player is on the ground, and checks to see if the audiosource is already playing a sound
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && audioSource.isPlaying == false)
        {
            //If it isn't already playing a sound and the player is moving.
            audioSource.Play();
        }
    }

    IEnumerator Walking()
    {
        //Random sound effect generation.
        while (coroutineOn == true)
        {

            if (cc.velocity.magnitude > 2f) //Check to see if the player is walking
            {
                int rand = Random.Range(0, audioClips.Length);
				currentClip = audioClips[rand];
                audioSource.Play(); //Play it
            }

            else
            {
                audioSource.Stop(); //If the player isn't moving, don't play the sound
            }

            yield return new WaitForSeconds(stepdelay); //And when the player is moving, only play every so often, not on every frame.

        }
    }
}
