using UnityEngine;

using System.Collections;

public class Enemy2Move : MonoBehaviour
{

    int speedAmt = 3;
    public float LeftLimit;
    public float RightLimit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x <= LeftLimit)
        {
            speedAmt = 3;
        }
        if (transform.position.x >= RightLimit)
        {
            speedAmt = -3;
        }
        transform.Translate(speedAmt * Time.deltaTime, 0, 0);


    }

}