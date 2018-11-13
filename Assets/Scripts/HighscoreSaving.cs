using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class HighscoreSaving : MonoBehaviour {

    PlayerData[] LB = new PlayerData[11];
    int LB_MAX = 10;


	// Use this for initialization
	void Start () {

        Load();  //Loads the information from the file if it exists

	}
	
	// Update is called once per frame
	void Update () {
		//Nothing to update, just needs to run once during load. 
	}



    public void Save(long newScore, string newName)
    {
        BinaryFormatter bf = new BinaryFormatter(); //Honestly unsure, but it works
        FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.OpenOrCreate); //Open or create the file if it doesn't exist

        PlayerData data = new PlayerData(newScore, newName); //Save a possible new entry as PlayerData Type
        //data.name = newName; //Save new name
        //data.highscore = newScore; //Save new score

        LeaderSort(data); //Sort the data like a Leaderboard

        bf.Serialize(file, data);
        file.Close();
    }
    
    public void Load()
    {
        PlayerData[] data = new PlayerData[10];


        if (File.Exists(Application.persistentDataPath + "/highScore.dat")) //If someone has played this before, and a highscore exists
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.Open);
            data = (PlayerData[]) bf.Deserialize(file);
            file.Close();

        }


        //Testing
        for(int i = 0; i < LB_MAX; i++)
        {
            data[i] = new PlayerData(i, "Testing");
        }



    }

    //Sort the leaderboard, and only keep the top 10 values.
    private void LeaderSort(PlayerData newPlayerData)
    {
        PlayerData[] temp = new PlayerData[11];
        PlayerData swap = null;
        temp[11] = newPlayerData; //Put it in there and sort
        

        for(int i = 0; i < temp.Length; i++)
            for(int j = 0; j < temp.Length; j++)
            {
                if(temp[i].highscore < temp[j].highscore)
                {
                    //Swap
                    swap = temp[i];
                    temp[i] = temp[j];
                    temp[j] = swap;
                }
            }

        //Shave off the extra that was added to see if it was part of the pack
        for(int i = 0; i < LB_MAX; i++)
        {
            LB[i] = temp[i];
        }


    }


    [Serializable]
    class PlayerData
    {
        public PlayerData(long newScore, string newName)
            {
                name = newName;
                highscore = newScore;
            }

        public long highscore;
        public string name;
    }

    [Serializable]
    class CurrentLeaderboard
    {
        PlayerData[] leaderboard;
    }

}
