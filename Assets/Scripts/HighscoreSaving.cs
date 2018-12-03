using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreSaving : MonoBehaviour {


    static int LB_MAX = 10;
    PlayerData[] LB = new PlayerData[LB_MAX];

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5; //All of the text that should be output to the Leaderboard
    public Text text6;
    public Text text7;
    public Text text8;
    public Text text9;
    public Text text10;
    
   



    // Use this for initialization
    void Start () {

        Load();  //Loads the information from the file if it exists
        //Save(10000, "Noah");
        //Save(20000, "Sarah");
    }
	
	// Update is called once per frame
	void Update () {
        //Nothing to update, just needs to run once during load. 


        

	}



    public void Save(long newScore, string newName)
    {
        BinaryFormatter bf = new BinaryFormatter(); //Honestly unsure, but it works
        FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.OpenOrCreate); //Open or create the file if it doesn't exist

        PlayerData newData = new PlayerData(newScore, newName); //Save a possible new entry as PlayerData Type
        //data.name = newName; //Save new name
        //data.highscore = newScore; //Save new score

        LeaderSort(newData); //Sort the data like a Leaderboard

        bf.Serialize(file, LB);
        file.Close();

        updateScores();
    }
    
    public void Load()
    {
        LB = new PlayerData[10];

        //Testing
        for (int i = 0; i < LB_MAX; i++)
        {
            LB[i] = new PlayerData(i, "Testing" + i);
        }


        if (File.Exists(Application.persistentDataPath + "/highScore.dat")) //If someone has played this before, and a highscore exists
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.Open);
            LB = (PlayerData[]) bf.Deserialize(file);
            file.Close();

        }

        //PlayerData fake = new PlayerData(0, "Bob");
        //LeaderSort(fake);
        updateScores();

        



    }

    //Sort the leaderboard, and only keep the top 10 values.
    private void LeaderSort(PlayerData newPlayerData)
    {
        PlayerData[] temp = new PlayerData[LB_MAX + 1];
        PlayerData swap = null;
        

        for(int i = 0; i < LB_MAX; i++)
        {
            temp[i] = LB[i];
        }
        temp[LB_MAX] = newPlayerData; //Put it in there and sort


        for (int i = 0; i < temp.Length; i++)
            for(int j = 0; j < temp.Length; j++)
            {
                if(temp[i].highscore > temp[j].highscore)
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

    void updateScores()
    {
        text1.text  = LB[0].toString();
        text2.text  = LB[1].toString();
        text3.text  = LB[2].toString();
        text4.text  = LB[3].toString();
        text5.text  = LB[4].toString();
        text6.text  = LB[5].toString();
        text7.text  = LB[6].toString();
        text8.text  = LB[7].toString();
        text9.text  = LB[8].toString();
        text10.text = LB[9].toString();



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


        public string toString()
        {
            string ans = "";
            ans += name + " " + highscore ;
            return ans;
        }

    }

    //[Serializable]
    //class CurrentLeaderboard
    //{
    //    public PlayerData[] leaderboard;
    //}


}
