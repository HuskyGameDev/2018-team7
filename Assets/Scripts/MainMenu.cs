using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameScene;
    public string controlsScene;
    public string leaderboardScene;

	// Use this for initialization
	void Start ()
	{
		GameController.ResetSeed();
	}
	
    public void Load()
    {
		if (File.Exists(Application.persistentDataPath + "/SaveGame.dat"))
		{
			GameController.pendingLoadGame = true;
			SceneManager.LoadScene(gameScene);
		}
    }

    public void Play()
    {
        SceneManager.LoadScene(gameScene);
    }

	public void Controls()
	{
		SceneManager.LoadScene(controlsScene);
	}

    public void Leaderboard()
    {
        // You've been hijacked for leaderboard - Noah
        SceneManager.LoadScene(leaderboardScene);
    }
}
