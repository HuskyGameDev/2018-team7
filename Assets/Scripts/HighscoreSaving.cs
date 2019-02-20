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
    public Text text6; //References to these are set inside the Unity Inspector
    public Text text7;
    public Text text8;
    public Text text9;
    public Text text10;
    
   



    // Use this for initialization
    void Start () {

        Load();  //Loads the information from the file if it exists
    }

    public void Save(long newScore, string newName)
    {
        BinaryFormatter bf = new BinaryFormatter(); //Honestly unsure, but it works
        FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.OpenOrCreate); //Open or create the file if it doesn't exist

        PlayerData newData = new PlayerData(newScore, newName); //Save a possible new entry as PlayerData Type

        LeaderSort(newData); //Sort the data like a Leaderboard
	//New person could have been added, but now it's sorted, and ready to put back
	    
        bf.Serialize(file, LB); //Make it able to be pushed to the file
        file.Close();

        //updateScores(); //Update what is shown on screen in case something changed
    }
    
    public void Load()
    {
        LB = new PlayerData[10]; //Empty the leaderboard before you load it in.

        if (File.Exists(Application.persistentDataPath + "/highScore.dat")) //If someone has played this before, and a highscore exists
        {
            BinaryFormatter bf = new BinaryFormatter();  //Open new formatter
            FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.Open); //Open the existing file
            LB = (PlayerData[]) bf.Deserialize(file); //Make the file readable to me again
            file.Close(); //Close the file because I read from it already. 

        }
	
	//I now have either nothing, or the new leaderboard. 
        updateScores(); //Update what is shown on screen

        



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

        //Shave off the extra that was added to see if it was part of the pack of 10, or whatever the max on the leaderboard is.
        for(int i = 0; i < LB_MAX; i++)
        {
            LB[i] = temp[i];
        }


    }

    void updateScores()
    {
	    
	//Literally just updates the text that's seen on screen.
        //text1.text  = LB[0].toString();
        //text2.text  = LB[1].toString();
        //text3.text  = LB[2].toString();
        //text4.text  = LB[3].toString();
        //text5.text  = LB[4].toString();
        //text6.text  = LB[5].toString();
        //text7.text  = LB[6].toString();
        //text8.text  = LB[7].toString();
        //text9.text  = LB[8].toString();
        //text10.text = LB[9].toString();



    }

    


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
