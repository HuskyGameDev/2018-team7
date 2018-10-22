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
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		move *= speed * Time.deltaTime;
		pc.Move(move);
    }
}



