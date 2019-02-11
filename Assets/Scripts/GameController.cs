using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	// Stores the seed being used to generate the current game.
	// This is static so that its value is preserved between scene changes.
	private static int seed = -1;

	public bool Paused { get; private set; }

	[SerializeField] private Text seedText;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject newLevelInput;

	public static GameController Instance { get; private set; }

	private void Awake()
	{
		Instance = this;

		// Awake is called when the game scene is loaded. If we have a seed set,
		// we want to use that seed - the level regenerated with a certain seed requested.
		SetSeed(seed == -1 ? Random.Range(int.MinValue, int.MaxValue) : seed);
	}

	private void SetSeed(int newSeed)
	{
		seed = newSeed;

		// Seeds the random generator.
		Random.InitState(newSeed);

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
	}
	
	// Linked to the pause menu UI. This is called when the UI button is clicked, and causes the
	// seed input UI to become active.
	public void NewLevelButtonHandler()
	{
		pauseMenu.SetActive(false);
		newLevelInput.SetActive(true);
	}

	// Called when the Play button from the pause menu UI is pressed.
	public void NewLevelPlayButtonHandler(InputField input)
	{
		if (input.text.Length > 0)
		{
			seed = int.Parse(input.text);
			Time.timeScale = 1.0f;
			SceneManager.LoadScene("Game");
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
}
