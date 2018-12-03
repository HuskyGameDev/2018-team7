using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private PlayerController playerController;
    private Floor floor;

    public Text text;

    // Use this for initialization
    void Start () {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        GameObject floorObject = GameObject.FindWithTag("Floor");

        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
            floor = floorObject.GetComponent<Floor>();

        }
        else
        {
            Debug.Log("Cannot find Player");
        }
       

	}
	
	// Update is called once per frame
	void Update () {
        text.text = floor.FloorID.ToString();
        float pos = ((playerController.health / 100f )*130f)-65f;
        this.transform.SetX(pos);
    }
}
