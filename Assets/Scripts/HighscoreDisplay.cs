using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour {


    //static int LB_MAX = 10;

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5; //All of the text that should be output to the Leaderboard
    public Text text6; //References to these are set inside the Unity Inspector
    public Text text7; //Because I'm a scrub and bad at Unity
    public Text text8;
    public Text text9;
    public Text text10;

    Leaderboard leaderboard = new Leaderboard();
    private string dataPath = "";
    



    // Use this for initialization
    void Start () {
        dataPath = Application.persistentDataPath + "/highScore.json";
        leaderboard = Load();
        updateScores();
	}

    public void ResetBoard()
    {
        leaderboard = ResetLeaderboard();
        updateScores();
    }

    void SaveToJson(Leaderboard newLead)
    {
        string dataAsJson = JsonUtility.ToJson(newLead);
        File.WriteAllText(dataPath, dataAsJson);
    }

    void updateScores()
    {

        //Literally just updates the text that's seen on screen.
        text1.text  = leaderboard.Player1.toString();
        text2.text  = leaderboard.Player2.toString();
        text3.text  = leaderboard.Player3.toString();
        text4.text  = leaderboard.Player4.toString();
        text5.text  = leaderboard.Player5.toString();
        text6.text  = leaderboard.Player6.toString();
        text7.text  = leaderboard.Player7.toString();
        text8.text  = leaderboard.Player8.toString();
        text9.text  = leaderboard.Player9.toString();
        text10.text = leaderboard.Player10.toString();
    }

    Leaderboard Load()
    {
        Leaderboard temp = new Leaderboard();
        if (File.Exists(dataPath))
        {
            string dataAsJson = File.ReadAllText(dataPath);
            temp = JsonUtility.FromJson<Leaderboard>(dataAsJson);
        }
        else
        {
            Debug.Log("Couldn't find the leaderboard file boss");
        }
        return temp;
    }

    Leaderboard ResetLeaderboard()
    {
        Leaderboard temp = new Leaderboard();
        temp.Player1 = new PlayerData(-1, "", false);
        temp.Player2 = new PlayerData(-1, "", false);
        temp.Player3 = new PlayerData(-1, "", false);
        temp.Player4 = new PlayerData(-1, "", false);
        temp.Player5 = new PlayerData(-1, "", false);
        temp.Player6 = new PlayerData(-1, "", false);
        temp.Player7 = new PlayerData(-1, "", false);
        temp.Player8 = new PlayerData(-1, "", false);
        temp.Player9 = new PlayerData(-1, "", false);
        temp.Player10 = new PlayerData(-1, "", false);
        SaveToJson(temp);
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

        //A way to print the information out. 
        public string toString()
        {
            string ans = "";
            ans += name + " " + highscore;
            return ans;
        }

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
