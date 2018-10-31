using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Author: Noah de Longpre' 
 * 
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
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
        {
            //If it isn't already playing a sound and the player is moving.
            GetComponent<AudioSource>().Play();

        }
    }

    IEnumerator Walking()
    {
        //Random sound effect generation.
        while (coroutineOn == true)
        {

            if (cc.velocity.magnitude > 2f) //Check to see if the player is walking
            {
                int rand = Random.Range(0, 12); //Randomize a number 0-11, 12 is not inclusive

                switch (rand) //What number is it?
                {
                    case 0: 
                        currentClip = audioClips[0];
                        break;
                    case 1:
                        currentClip = audioClips[1];
                        break;

                    case 2:
                        currentClip = audioClips[2];
                        break;

                    case 3:
                        currentClip = audioClips[3];
                        break;

                    case 4:
                        currentClip = audioClips[4];
                        break;

                    case 5:
                        currentClip = audioClips[5];
                        break;

                    case 6:
                        currentClip = audioClips[6];
                        break;

                    case 7:
                        currentClip = audioClips[7];
                        break;

                    case 8:
                        currentClip = audioClips[8];
                        break;

                    case 9:
                        currentClip = audioClips[9];
                        break;

                    case 10:
                        currentClip = audioClips[10];
                        break;

                }

                audioSource.clip = currentClip; //Tell the audio source that this is the one we picked

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
