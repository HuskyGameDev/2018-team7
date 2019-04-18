using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
	// Stores the seed being used to generate the current game.
	// This is static so that its value is preserved between scene changes.
	public static int seed { get; private set; } = -1;

	public static bool pendingLoadGame;

	public const int MaxSeed = 100000000;

	public bool Paused { get; private set; }

	[SerializeField] private Text seedText;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject newLevelInput;

	private int score;

	public static GameController Instance { get; private set; }

	private bool soundMuted;

	private void Awake()
	{
		Instance = this;

		if (pendingLoadGame)
		{
			LoadGame();
			pendingLoadGame = false;
		}

		// Awake is called when the game scene is loaded. If we have a seed set,
		// we want to use that seed - the level regenerated with a certain seed requested.
		SetSeed(seed == -1 ? Random.Range(0, MaxSeed) : seed);
	}

    private static void ClearSaveData()
    {
        File.Delete(Application.persistentDataPath + "/SaveGame.dat");
    }

	public static void ResetGame()
	{
		seed = -1;
        ClearSaveData();
		Floor.Instance.saveData = null;
	}

	public void ResetSeed()
	{
		seed = -1;
	}

	private void SetSeed(int newSeed)
	{
		seed = newSeed;
		seedText.text = "Seed: " + newSeed.ToString();
	}

	/// <summary>
	/// Add to the existing score value.
	/// </summary>
	public void AddScore(int value)
	{
		score += value;
	}

	/// <summary>
	/// Overwrite the existing score value with a new one.
	/// </summary>
	public void SetScore(int value)
	{
		score = value;
	}

    public int GetScore()
    {
        return score;
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
			int value;
			if (int.TryParse(input.text, out value))
			{
				if (value >= 0 && value < MaxSeed)
				{
					seed = value;
					Time.timeScale = 1.0f;
					SceneManager.LoadScene("Game");
					return;
				}
			}
		}

		input.text = "";
	}

	/// <summary>
	/// Linked to the pause menu UI. If the sound is currently muted, unmutes it. 
	/// Otherwise, mutes it. This mutes all sound in the game from all sources.
	/// </summary>
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

	/// <summary>
	/// Linked to the pause menu. Loads the main menu scene.
	/// </summary>
	public void MainMenuButtonHandler()
	{
		// Unpause resets Time.timeScale, which is global.
		Unpause();

		SceneManager.LoadScene("MainMenu");
	}

	public void SaveGame()
	{
		SaveData newSave = new SaveData(null, null, null, null);
		newSave.floor = Floor.Instance.FloorID;
		newSave.seed = seed;

		PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		newSave.guns = pc.GetGunArray();
		newSave.gunAmmo = pc.GetGumAmmoArray();
		newSave.score = score;

		BinaryFormatter bf = new BinaryFormatter(); //Honestly unsure, but it works
		FileStream file = File.Open(Application.persistentDataPath + "/SaveGame.dat", FileMode.OpenOrCreate); //Open or create the file if it doesn't exist

		bf.Serialize(file, newSave); //Make it able to be pushed to the file
		file.Close();
	}

	private void LoadGame()
	{
		SaveData loaded = new SaveData(null, null, null, null); //empty savedata to add the file info to

		if (File.Exists(Application.persistentDataPath + "/SaveGame.dat")) //If someone has played this before, and a highscore exists
		{
			BinaryFormatter bf = new BinaryFormatter();  //Open new formatter
			FileStream file = File.Open(Application.persistentDataPath + "/SaveGame.dat", FileMode.Open); //Open the existing file
			loaded = (SaveData)bf.Deserialize(file); //Make the file readable to me again
			file.Close(); //Close the file because I read from it already. 

			SetSeed(loaded.seed ?? Random.Range(0, MaxSeed));

			PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
			pc.SetGunArray(loaded.guns);
			pc.SetGunAmmo(loaded.gunAmmo);

			score = loaded.score ?? 0;
		}

		Floor floor = Floor.Instance;
		floor.saveData = loaded;
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

	private void OnDestroy()
	{
		PlayerPrefs.SetInt("Score", score);
	}
}

[Serializable]
public class SaveData
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
	public int[] gunAmmo;
}