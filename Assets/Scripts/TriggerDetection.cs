using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour {

    public PlayerController pc;

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
        Debug.Log("Test");
        if (other.name == "TestPlayer")
        {
            pc.setHealth(pc.getHealth() - 10);
        }
    }
}
