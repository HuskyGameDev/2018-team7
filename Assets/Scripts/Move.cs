using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {


    public float speed;

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public float getSpeed()
    {
        return this.speed;
    }

    // Use this for initialization
    void Start()
    {
        speed = .03f;
    }

    //NOT FINAL
    // Update is called once per frame
    //Moves the player with the arrow keys and WASD keys
    //Player can choose either
    void Update()
    {
        if (Input.GetKey(KeyCode.A))

        {

            gameObject.transform.Translate(Vector3.left * speed);

        }

        if (Input.GetKey(KeyCode.D))

        {

            gameObject.transform.Translate(Vector3.right * speed);

        }

        if (Input.GetKey(KeyCode.W))

        {

            gameObject.transform.Translate(Vector3.up * speed);

        }

        if (Input.GetKey(KeyCode.S))

        {

            gameObject.transform.Translate(Vector3.down * speed);

        }

    }
}



