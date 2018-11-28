using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class Move : MonoBehaviour {

    public CharacterController pc;

	public float speed { get; set; }
	public float friction;

	private Vector2 velocity;

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
        pc = GetComponent<CharacterController>();
        speed = 100f;
    }

	public void ApplyKnockback(Vector3 dir, float force)
	{
		velocity = dir * force;
	}

    void Update()
    {
		float currentSpeed = speed;

		if (Input.GetKey(KeyCode.LeftShift))
			currentSpeed *= 3.0f;

		Vector2 accel = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		float len = accel.sqrMagnitude;

		if (len > 1.0f)
			accel *= (1.0f / Mathf.Sqrt(len));

		accel *= currentSpeed;
		accel += (velocity * friction);

		Vector2 delta = accel * 0.5f * Square(Time.deltaTime) + velocity * Time.deltaTime;
		velocity = accel * Time.deltaTime + velocity;

		pc.Move(delta);
    }
}



