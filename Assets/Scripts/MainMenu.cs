using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameScene;
    public string loadoutScene;
    public string settingsScene;

	// Use this for initialization
	void Start ()
	{
		GameController.ResetSeed();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Loadout()
    {
        SceneManager.LoadScene(loadoutScene);
    }

    public void Play()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void Settings()
    {
        SceneManager.LoadScene(settingsScene);
    }
}
