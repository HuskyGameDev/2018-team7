using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private HighscoreSaving HS;
    private Stats stats;

    public InputField IF;
    public Text score;

    public void Awake()
    {
        score.text = "Score = " + PlayerPrefs.GetInt("Score");
    }

    public void PlayAgain()
	{
		GameController.ResetSeed();
        SceneManager.LoadScene("Game");
	}

	public void Menu()
	{
		GameController.ResetSeed();
		SceneManager.LoadScene("MainMenu");
	}

    public void SaveGame()
    {
        GameObject InputFieldGO = GameObject.FindWithTag("InputField");
        IF = InputFieldGO.GetComponent<InputField>();
        HS.SaveScore(PlayerPrefs.GetInt("Score"), IF.text);
    }
}
