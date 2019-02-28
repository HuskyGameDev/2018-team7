using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class Move : MonoBehaviour {

    public CharacterController pc;
	private PlayerController playerController;

	public float speed { get; set; }
	public float SpeedModifier { get; set; } = 1.0f;

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
		playerController = GetComponent<PlayerController>();
        speed = 100f;
    }

	public void ApplyKnockback(Vector3 dir, float force)
	{
		velocity = dir * force;
	}

    void Update()
    {
		if (Time.timeScale == 0.0f)
			return;

		float currentSpeed = speed * SpeedModifier;

		if (Input.GetKey(KeyCode.LeftShift))
			currentSpeed *= 2.0f;

		Vector2 accel = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		playerController.UpdateSprite(accel);

		float len = accel.sqrMagnitude;

		if (len > 1.0f)
			accel *= (1.0f / Mathf.Sqrt(len));

		accel *= currentSpeed;
		accel += (velocity * friction);

		Vector2 delta = accel * 0.5f * Square(Time.deltaTime) + velocity * Time.deltaTime;
		velocity = accel * Time.deltaTime + velocity;

		pc.Move(delta);
		transform.SetZ(-0.1f);
    }
}



