using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreSaving : MonoBehaviour {

    public static HighscoreSaving ScoreSaving;

    static int LB_MAX = 10;
    PlayerData[] LB = new PlayerData[LB_MAX];

    void Awake()
    {
        if (ScoreSaving == null)
        {
            ScoreSaving = this;
        }
    }


    // Use this for initialization
    void Start () {

        int score = PlayerPrefs.GetInt("Score");
        string name = PlayerPrefs.GetString("Name");
		Debug.Log("Score: " + score);
        Debug.Log("Name: " + name);
        SaveScore(score, name);
    }

    public void SaveScore(long newScore, string newName)
	{
        BinaryFormatter bf = new BinaryFormatter(); //It's what writes or reads from the file. 

        FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.OpenOrCreate); //Open or create the file if it doesn't exist

		try
		{
			PlayerData newData = new PlayerData(newScore, newName); //Save a possible new entry as PlayerData Type

			LeaderSort(newData); //Sort the data like a Leaderboard
								 //New person could have been added, but now it's sorted, and ready to put back
		}
		catch (NullReferenceException)
		{
			Debug.Log("TODO: Fix this NullReferenceException!");
		}
		finally
		{
			bf.Serialize(file, LB); //Make it able to be pushed to the file
			file.Close();
		}
    }
    

    //Sort the leaderboard, and only keep the top 10 values.
    private void LeaderSort(PlayerData newPlayerData)
    {
        PlayerData[] temp = new PlayerData[LB_MAX + 1]; //Set it to be bigger than what the max is
        PlayerData swap = null; 
        
	//Fill the temp array with real values so I don't manipulate current real data. 
        for(int i = 0; i < LB_MAX; i++)
        {
            
            temp[i] = LB[i];
         
                
        }
        temp[LB_MAX] = newPlayerData; //Put the new value on the end.

	
	//Basic bubble sort to sort the 11 values that could exist.
        for (int i = 0; i < temp.Length; i++)
        {
            for (int j = 0; j < temp.Length; j++)
            {
                if (temp[i].highscore > temp[j].highscore )
                {
                    //Swap
                    swap = temp[i];
                    temp[i] = temp[j];
                    temp[j] = swap;
                }
                
            }
        }

        //Shave off the extra that was added to see if it was part of the pack of 10, or whatever the max on the leaderboard is.
        for (int i = 0; i < LB_MAX; i++)
        {
            LB[i] = temp[i];
        }


    }


    /*
    else if(temp[i].highscore == null && temp[j].highscore != null)
                {
                    temp[i] = temp[j];
                }
                else if (temp[i].highscore != null && temp[j].highscore == null)
                {
                    //Do nothing? 
                }

        && temp[i].highscore != null && temp[j].highscore != null

    */
    [Serializable] //Make it Serializable? Easier to put into a file. 
    class PlayerData
    {
	//Private class to hold data about player
        public PlayerData(long newScore, string newName)
            {
                name = newName;
                highscore = newScore;
            }

        public long highscore;
        public string name;

	//A way to print the information out. 
        public string toString()
        {
            string ans = "";
            ans += name + " " + highscore ;
            return ans;
        }

    }



}
