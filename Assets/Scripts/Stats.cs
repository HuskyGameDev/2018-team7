using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float health = 100f;
    public float shooting_speed = .05f;
    public bool ricochet = false;
    public long score = 0;


    private void Awake()
    {
        DontDestroyOnLoad(this); //When you load into the GameOver scene, don't destroy this gameObject.
    }

    public void setScore(long newScore)
    {
        score = newScore; //A way to set the score in the stats file.
    }

    
    //Reset the stats back to what they were to start, (For play again, or initial load.)
    public void resetStats()
    {
      health = 100f;
      shooting_speed = .05f;
      ricochet = false;
      score = 0;
    }

        
}
