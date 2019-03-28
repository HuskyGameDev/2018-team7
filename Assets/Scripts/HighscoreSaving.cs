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
    //PlayerData[] LB = new PlayerData[LB_MAX];
    Leaderboard leaderboard;
    private string dataPath = "";
    

    void Awake()
    {
        if (ScoreSaving == null)
        {
            ScoreSaving = this;
        }
    }


    // Use this for initialization
    void Start () {
        dataPath = Application.persistentDataPath + "/highScore.json";
        //int score = PlayerPrefs.GetInt("Score");
        //string name = PlayerPrefs.GetString("Name");


        //leaderboard = ResetLeaderboard();

		
    }

    public void ResetBoard()
    {
        leaderboard = ResetLeaderboard();
    }

    Leaderboard ResetLeaderboard()
    {
        Leaderboard temp = new Leaderboard();
        temp.Player1  = new PlayerData(-1, "", false);
        temp.Player2  = new PlayerData(-1, "", false);
        temp.Player3  = new PlayerData(-1, "", false);
        temp.Player4  = new PlayerData(-1, "", false);
        temp.Player5  = new PlayerData(-1, "", false);
        temp.Player6  = new PlayerData(-1, "", false);
        temp.Player7  = new PlayerData(-1, "", false);
        temp.Player8  = new PlayerData(-1, "", false);
        temp.Player9  = new PlayerData(-1, "", false);
        temp.Player10 = new PlayerData(-1, "", false);
        SaveToJson(temp);
        return temp;
    }

    public void SaveScore(long newScore, string newName, bool realPlay)
	{
        PlayerData newData = new PlayerData(newScore, newName, realPlay); // newScore, newName Save a possible new entry as PlayerData Type
        //newData.highscore = newScore;
        //newData.name = newName;
        Debug.Log("Score: " + newScore);
        Debug.Log("Name: " + newName);
        Debug.Log("Real Player? = " + realPlay);
        LeaderSort(newData); //Sorts and saves into leaderboard variable
        SaveToJson(leaderboard); //Save all of that information to the file. 
        
    }

    void SaveToJson(Leaderboard newLead)
    {
        string dataAsJson = JsonUtility.ToJson(newLead);
        File.WriteAllText(dataPath, dataAsJson);
    }

    private void LeaderSort(PlayerData newPlayerData)
    {
        PlayerData[] sort = new PlayerData[LB_MAX + 1]; //Make 
        Leaderboard temp = LoadForSort();
        //Hardcoded to help?
        sort[0] = temp.Player1;
        sort[1] = temp.Player2;
        sort[2] = temp.Player3;
        sort[3] = temp.Player4;
        sort[4] = temp.Player5;
        sort[5] = temp.Player6;
        sort[6] = temp.Player7;
        sort[7] = temp.Player8;
        sort[8] = temp.Player9;
        sort[9] = temp.Player10;
        sort[10] = newPlayerData;

        for (int i = 0; i < LB_MAX + 1; i++)
        {
            Debug.Log("sort[" + i + "] = " + sort[i].name);
        }

        PlayerData swap;

        for(int i = 0; i < LB_MAX + 1; i++)
        {
            for(int j = 0; j < LB_MAX + 1; j++)
            {
                if(sort[i].highscore > sort[j].highscore )
                {
                    //Swap
                    swap = sort[i];
                    sort[i] = sort[j];
                    sort[j] = swap;
                }
            }
        }


        //Should be sorted, put back into the leaderboard structure for saving. 
        temp.Player1 = sort[0];
        temp.Player2 = sort[1];
        temp.Player3 = sort[2];
        temp.Player4 = sort[3];
        temp.Player5 = sort[4];
        temp.Player6 = sort[5];
        temp.Player7 = sort[6];
        temp.Player8 = sort[7];
        temp.Player9 = sort[8];
        temp.Player10 = sort[9];

        leaderboard = temp; //Save it outside this scope




    }



    Leaderboard LoadForSort()
    {
        Leaderboard temp = new Leaderboard();
        if (File.Exists(dataPath))
        {
            string dataAsJson = File.ReadAllText(dataPath);
            temp = JsonUtility.FromJson<Leaderboard>(dataAsJson);
        }
        else
        {
            temp = ResetLeaderboard();
        }
        return temp;
    }


    [Serializable] //Make it Serializable? Easier to put into a file. 
    class PlayerData
    {
        //Private class to hold data about player
        
            public PlayerData(long newScore, string newName, bool newReal)
                {
                    name = newName;
                    highscore = newScore;
                    realPlayer = newReal;
                }

        public long highscore = 0;
        public string name = "Test";
        public bool realPlayer = false; 
   
    }

    [Serializable]
    class Leaderboard
    {
        public PlayerData Player1;
        public PlayerData Player2;
        public PlayerData Player3;
        public PlayerData Player4;
        public PlayerData Player5;
        public PlayerData Player6;
        public PlayerData Player7;
        public PlayerData Player8;
        public PlayerData Player9;
        public PlayerData Player10;
    }



}
