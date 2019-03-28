using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameScene;
    public string loadoutScene;
    public string settingsScene;
    public Scene leaderboard;

	// Use this for initialization
	void Start ()
	{
		GameController.ResetSeed();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Load()
    {
       

        
        SceneManager.LoadScene(gameScene);
    }

    public void Play()
    {
        
        SceneManager.LoadScene(gameScene);
    }
    public void Settings()
    {
        //You've been hijacked for leaderboard - Noah
        SceneManager.LoadScene(settingsScene);
    }


    
}
