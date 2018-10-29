using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klusterfunk : MonoBehaviour
{
    public int pelletCount;
    public float spreadAngle;
    public float pelletFireVel = 1;
    public GameObject pellet;
    public Transform BarrelExit;
   

    List<Quaternion> pellets;

    // Use this for initialization
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

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Fire();
        }

    }
    void Fire()
    {
        int i = 0;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            foreach (Quaternion quat in pellets)
            {
                pellets[i] = Random.rotation;
                GameObject p = Instantiate(pellet, BarrelExit.position, BarrelExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(p.transform.up * pelletFireVel);
                i++;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            foreach (Quaternion quat in pellets)
            {
                pellets[i] = Random.rotation;
                GameObject p = Instantiate(pellet, BarrelExit.position, BarrelExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(-(p.transform.up * pelletFireVel));
                i++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            foreach (Quaternion quat in pellets)
            {
                pellets[i] = Random.rotation;
                GameObject p = Instantiate(pellet, BarrelExit.position, BarrelExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(-(p.transform.right * pelletFireVel));
                i++;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            foreach (Quaternion quat in pellets)
            {
                pellets[i] = Random.rotation;
                GameObject p = Instantiate(pellet, BarrelExit.position, BarrelExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * pelletFireVel);
                i++;
            }
        }
    }

}
