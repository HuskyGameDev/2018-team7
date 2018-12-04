using UnityEngine;
using System.Collections.Generic;

public class SMG : Gun
{
    public float timeBetweenShots = 0.11111f;
	private int pelletCount = 1;
    public float spreadAngle;
	public float pelletFireVel = 500;
    public Transform BarrelExit;
    private float timestamp;

    List<Quaternion> pellets;

	protected override void Init()
	{
		speed = 0.1f;
	}

	// Update is called once per frame
	void Awake()
    {
        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    // Update is called once per frame
    void Update()
	{
		//Shoots Right
		if (Time.time >= timestamp && Input.GetKey(KeyCode.RightArrow))
		{
			for (int i = 0; i < pellets.Count; i++)
			{
				timestamp = Time.time + timeBetweenShots;
				pellets[i] = Random.rotation;
				GameObject p = CreateBullet(transform, transform.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<Rigidbody>().isKinematic = false; // NOTE: temp fix to make it work with 3D physics quickly. We probably shouldn't be using forces, though...
				p.GetComponent<Rigidbody>().AddForce(Vector2.right * pelletFireVel);
			}
		}

		//Shoots Left
		else if (Time.time >= timestamp && Input.GetKey(KeyCode.LeftArrow))
		{
			for (int i = 0; i < pellets.Count; i++)
			{
				timestamp = Time.time + timeBetweenShots;
				pellets[i] = Random.rotation;
				GameObject p = CreateBullet(transform, transform.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<Rigidbody>().isKinematic = false; // NOTE: temp fix to make it work with 3D physics quickly. We probably shouldn't be using forces, though...
				p.GetComponent<Rigidbody>().AddForce(Vector2.left * pelletFireVel);
			}

		}
		//Shoots Up
		else if (Time.time >= timestamp && Input.GetKey(KeyCode.UpArrow))
		{
			for (int i = 0; i < pellets.Count; i++)
			{
				timestamp = Time.time + timeBetweenShots;
				pellets[i] = Random.rotation;
				GameObject p = CreateBullet(transform, transform.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<Rigidbody>().isKinematic = false; // NOTE: temp fix to make it work with 3D physics quickly. We probably shouldn't be using forces, though...
				p.GetComponent<Rigidbody>().AddForce(Vector2.up * pelletFireVel);
			}
		}
		//Shoots Down
		else if (Time.time >= timestamp && Input.GetKey(KeyCode.DownArrow))
		{
			for (int i = 0; i < pellets.Count; i++)
			{
				timestamp = Time.time + timeBetweenShots;
				pellets[i] = Random.rotation;
				GameObject p = CreateBullet(transform, transform.rotation);
				p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
				p.GetComponent<Rigidbody>().isKinematic = false; // NOTE: temp fix to make it work with 3D physics quickly. We probably shouldn't be using forces, though...
				p.GetComponent<Rigidbody>().AddForce(Vector2.down * pelletFireVel);
			}
		}
	}
}
