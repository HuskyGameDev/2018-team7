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
        DontDestroyOnLoad(this);
    }

    public void setScore(long newScore)
    {
        score = newScore;
    }

    public void resetStats()
    {
      health = 100f;
      shooting_speed = .05f;
      ricochet = false;
      score = 0;
    }

        
}
