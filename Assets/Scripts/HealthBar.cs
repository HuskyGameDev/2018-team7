using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private PlayerController playerController;

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
       

	}
	
	// Update is called once per frame
	void Update () {
        float pos = ((playerController.health / 100f )*130f)-65f;
        this.transform.SetX(pos);
    }
}
