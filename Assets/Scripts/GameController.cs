using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	private static int seed = -1;

	public bool Paused { get; private set; }

	[SerializeField] private Text seedText;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject newLevelInput;

	public static GameController Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		SetSeed(seed == -1 ? Random.Range(int.MinValue, int.MaxValue) : seed);
	}

	private void SetSeed(int newSeed)
	{
		seed = newSeed;
		Random.InitState(newSeed);
		seedText.text = "Seed: " + newSeed.ToString();
	}

	public void Pause()
	{
		Paused = true;
		Time.timeScale = 0.0f;
		pauseMenu.SetActive(true);
	}

	public void Unpause()
	{
		Paused = false;
		Time.timeScale = 1.0f;
		pauseMenu.SetActive(false);
	}

	public void NewLevelButtonHandler()
	{
		pauseMenu.SetActive(false);
		newLevelInput.SetActive(true);
	}

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
