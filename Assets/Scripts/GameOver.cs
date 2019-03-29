using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //public GameObject SavingObject;
    public HighscoreSaving HS;
    //private HighscoreSaving HS;
    private Stats stats;

    public InputField IF;
    public Text score;
    public Text nameText;
    public Text SaveSuccess;

	public InputField input;

    public void Awake()
    {
        score.text = "Score = " + PlayerPrefs.GetInt("Score");
        //HS = SavingObject;
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

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (!input.text.Equals(string.Empty))
				SaveGame();
		}
	}

	public void SaveGame()
    {
        //GameObject InputFieldGO = GameObject.FindWithTag("InputField");
        //IF = InputFieldGO.GetComponent<InputField>(); IF.text
        HS.SaveScore(PlayerPrefs.GetInt("Score"), nameText.text, true);
        Debug.Log("Name:" + nameText.text + " Score: " + PlayerPrefs.GetInt("Score"));
        SaveSuccess.text = "Save Successful!";
        
    }
}
