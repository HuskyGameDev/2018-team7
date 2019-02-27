using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour {


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
        Load();		
	}
	
	// Update is called once per frame
	void Update () {
		
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

    void updateScores()
    {

        //Literally just updates the text that's seen on screen.
        text1.text = LB[0].toString();
        text2.text = LB[1].toString();
        text3.text = LB[2].toString();
        text4.text = LB[3].toString();
        text5.text = LB[4].toString();
        text6.text = LB[5].toString();
        text7.text = LB[6].toString();
        text8.text = LB[7].toString();
        text9.text = LB[8].toString();
        text10.text = LB[9].toString();
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
            ans += name + " " + highscore;
            return ans;
        }

    }



}
