using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minigun : Gun
{
    public float timeBetweenShots = 0.05f;
	public int pelletCount;
    public float spreadAngle;
    public float pelletFireVel = 1;
    public Transform BarrelExit;
    public GameObject bullet;


    List<Quaternion> pellets;


    private float timestamp;

	protected override void Init()
	{
		speed = 0.2f;
	}

	// Update is called once per frame
	void Update () {
		 if (Time.time >= timestamp && Input.GetButton("Fire1"))
        {
            Fire();
        }
        else if (Time.time >= timestamp && Input.GetButton("Fire2"))
        {
            Fire();
        }
        else if (Time.time >= timestamp && Input.GetButton("Fire3"))
        {
            Fire();
        }
        else if (Time.time >= timestamp && Input.GetButton("Fire4"))
        {
            Fire();
        }
	}
  void Fire()
    {
        int i = 0;

        if (Time.time >= timestamp && Input.GetButton("Fire1"))
        {
            foreach (Quaternion quat in pellets)
            {
                timestamp = Time.time + timeBetweenShots;
                pellets[i] = Random.rotation;
                GameObject p = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(p.transform.up * pelletFireVel);
                i++;
            }
        }
        if (Time.time >= timestamp && Input.GetButton("Fire2"))
        {
            foreach (Quaternion quat in pellets)
            {
                timestamp = Time.time + timeBetweenShots;
                pellets[i] = Random.rotation;
                GameObject p = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(-(p.transform.up * pelletFireVel));
                i++;
            }
        }
        if (Time.time >= timestamp && Input.GetButton("Fire3"))
        {
            foreach (Quaternion quat in pellets)
            {
                timestamp = Time.time + timeBetweenShots;
                pellets[i] = Random.rotation;
                GameObject p = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(-(p.transform.right * pelletFireVel));
                i++;
            }
        }
        if (Time.time >= timestamp && Input.GetButton("Fire4"))
        {
            foreach (Quaternion quat in pellets)
            {
                timestamp = Time.time + timeBetweenShots;
                pellets[i] = Random.rotation;
                GameObject p = Instantiate(bullet, BarrelExit.position, BarrelExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * pelletFireVel);
                i++;
            }
        }
    }

}