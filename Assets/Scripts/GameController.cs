using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
	// Stores the seed being used to generate the current game.
	// This is static so that its value is preserved between scene changes.
	private static int seed = -1;

	public const int MaxSeed = 100000000;

	public bool Paused { get; private set; }

	[SerializeField] private Text seedText;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject newLevelInput;

	public static GameController Instance { get; private set; }

	private bool soundMuted;

	private void Awake()
	{
		Instance = this;

		// Awake is called when the game scene is loaded. If we have a seed set,
		// we want to use that seed - the level regenerated with a certain seed requested.
		SetSeed(seed == -1 ? UnityEngine.Random.Range(0, MaxSeed) : seed);
	}

	public static void ResetSeed()
	{
		seed = -1;
	}

	private void SetSeed(int newSeed)
	{
		seed = newSeed;

        // Seeds the random generator.
        UnityEngine.Random.InitState(newSeed);

		seedText.text = "Seed: " + newSeed.ToString();
	}

	public void Pause()
	{
		Paused = true;

		// Setting time scale to 0 stops Unity's time entirely, which does most of the work to pause the game
		// (as long as code is using Unity's time to update, ie with Time.deltaTime). 
		Time.timeScale = 0.0f;

		pauseMenu.SetActive(true);
	}

	public void Unpause()
	{
		Paused = false;
		Time.timeScale = 1.0f;
		pauseMenu.SetActive(false);
		newLevelInput.SetActive(false);
	}
	
	// Linked to the pause menu UI. This is called when the UI button is clicked, and causes the
	// seed input UI to become active.
	public void NewLevelButtonHandler()
	{
		pauseMenu.SetActive(false);
		newLevelInput.SetActive(true);
		newLevelInput.transform.Find("InputField").GetComponent<InputField>().Select();
	}

	// Called when the Play button from the pause menu UI is pressed.
	public void NewLevelPlayButtonHandler(InputField input)
	{
		if (input.text.Length > 0)
		{
			if (int.TryParse(input.text, out seed))
			{
				if (seed >= 0 && seed < MaxSeed)
				{
					Time.timeScale = 1.0f;
					SceneManager.LoadScene("Game");
					return;
				}
			}
		}

		input.text = "";
	}

	public void MuteSoundButtonHandler(Text buttonText)
	{
		if (soundMuted)
		{
			AudioListener.volume = 1.0f;
			soundMuted = false;
			buttonText.text = "Mute Sound";
		}
		else
		{
			AudioListener.volume = 0.0f;
			soundMuted = true;
			buttonText.text = "Unmute Sound";
		}
	}

    public void MainMenuButtonHandler()
    {
        Unpause();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuickSaveButtonHandler()
    {
        //TODO:populate object with relavent data
        SaveData newSave = new SaveData(null,null,null,null) ;
        newSave.floor = Floor.Instance.FloorID;
        newSave.seed = seed;

        PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        newSave.guns = pc.GetGunArray();
        newSave.score = newSave.floor;

        BinaryFormatter bf = new BinaryFormatter(); //Honestly unsure, but it works
        FileStream file = File.Open(Application.persistentDataPath + "/savegame.dat", FileMode.OpenOrCreate); //Open or create the file if it doesn't exist

         

        bf.Serialize(file, newSave); //Make it able to be pushed to the file
        file.Close();

        //updateScores(); //Update what is shown on screen in case something changed
    }
    //
    public void QuickLoadButtonHandler()
    {
        SaveData loaded = new SaveData(null,null,null,null) ; //empty savedata to add the file info to

        if (File.Exists(Application.persistentDataPath + "/highScore.dat")) //If someone has played this before, and a highscore exists
        {
            BinaryFormatter bf = new BinaryFormatter();  //Open new formatter
            FileStream file = File.Open(Application.persistentDataPath + "/SaveGame.dat", FileMode.Open); //Open the existing file
            loaded = (SaveData)bf.Deserialize(file); //Make the file readable to me again
            file.Close(); //Close the file because I read from it already. 

            seed = loaded.seed??0;//set seed to the loaded seed, if loaded one is null somehow set it to 0

            Floor.Instance.FloorID = loaded.floor ?? 1;

            PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            pc.SetGunArray(loaded.guns);


            //Time.timeScale = 1.0f;
            //SceneManager.LoadScene("Game");
            return;


        }
    }


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Paused)
				Unpause();
			else Pause();
		}
	}

    [Serializable]
    class SaveData
    {
        //Private class to hold data about player
        public SaveData(int? seed, int? floor, int? score, bool[] guns)
        {
            this.seed = seed;
            this.floor = floor;
            this.score = score;
            this.guns = guns;
        }

        public int? seed;
        public int? score;
        public int? floor;
        public bool[] guns;



    }
   }

 
