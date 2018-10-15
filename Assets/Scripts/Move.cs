using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public CharacterController pc;

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
        speed = 10f;
        pc = GetComponent<CharacterController>();
    }

    //NOT FINAL
    // Update is called once per frame
    //Moves the player with the arrow keys and WASD keys
    //Player can choose either
    void Update()
    {
        if (Input.GetKey(KeyCode.A))

        {

            //gameObject.transform.Translate(Vector3.left * speed);
            pc.Move(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))

        {

            //gameObject.transform.Translate(Vector3.right * speed);
            pc.Move(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))

        {

            //gameObject.transform.Translate(Vector3.up * speed);
            pc.Move(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))

        {

            //gameObject.transform.Translate(Vector3.down * speed);
            pc.Move(Vector3.down * speed * Time.deltaTime);
        }

    }
}



