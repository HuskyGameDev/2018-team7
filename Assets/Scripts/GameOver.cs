using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private HighscoreSaving HS;
    private Stats stats;
    long score;
   


    public void PlayAgain()
	{
		//SaveRun();
		GameController.ResetSeed();
        SceneManager.LoadScene("Game");
	}

	public void Menu()
	{
		//SaveRun();
		GameController.ResetSeed();
		SceneManager.LoadScene("MainMenu");
	}

    /* Trying to figure out how to get this information after it changes scene.
     * 
     * https://forum.unity.com/threads/how-to-pass-information-from-one-scene-to-another.23344/
     * 
     *
    public void SaveRun()
    {
        score = stats.score;
        HS.Save(score, "TestyTest420");
        stats.resetStats();
    }

    public void Awake()
    {
        GameObject highScoreObject = GameObject.FindWithTag("HighscoreSaving");
        GameObject statsObject = GameObject.FindWithTag("Stats");
        HS = highScoreObject.GetComponent<HighscoreSaving>();
        stats = statsObject.GetComponent<Stats>();

    }
    */

}
