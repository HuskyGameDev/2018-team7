using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Image pistolSlot, smgSlot, shotgunSlot, sniperSlot, minigunSlot; // The possible imagesP0
    public Image [] imageList; // image list to initialize the images
    public string[] imageNames = { "PistolSlot", "ShotgunSlot", "SMGSlot", "SniperSlot", "MinigunSlot" };  // Current list of gun names
    public PlayerController playerController; 


    // Use this for initialization
    void Start () {
        imageList = new Image[5];

        // Get the player controller to find gun enablers
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Cannot find Player");
        }

        bool loading = false; // if loading the images works

        // Initialize all of the images to the correct object
        for (int i = 0; i < imageList.Length; i++)
        {
            GameObject temp = GameObject.Find(imageNames[i]);
            if (temp != null)
            {
                imageList[i] = temp.GetComponent<Image>();
                if (i != 0)
                    imageList[i].enabled = false;
                loading = true; // if loading works
            }
            else
            {
                Debug.Log("Loading UI Indicators Failed");
                loading = false;
                break;
            }
        }

        // Set the image objects for readability
        if (loading)
        {
            pistolSlot = imageList[0];
            shotgunSlot = imageList[1];
            smgSlot = imageList[2];
            sniperSlot = imageList[3];
            minigunSlot = imageList[4];
        }

    }
	
	// Update is called once per frame
    // Update the UI to display the image for each of the objects
	void Update () {
		if (playerController.shotgun)
            shotgunSlot.enabled = true;
       
        if (playerController.smg)
            smgSlot.enabled = true;

        if (playerController.shotgun)
            shotgunSlot.enabled = true;

        if (playerController.sniper)
            sniperSlot.enabled = true;

        if (playerController.minigun)
            minigunSlot.enabled = true;
	}
}
