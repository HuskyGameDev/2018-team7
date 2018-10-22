using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour {

    private PlayerController pc;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            pc = playerControllerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Cannot find Player");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with something.");
        if (other.transform.parent.CompareTag("Player"))
        {
			Debug.Log("Collided with the player.");
            pc.setHealth(pc.getHealth() - 10);
        }
    }
}
