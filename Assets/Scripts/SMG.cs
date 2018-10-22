using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : MonoBehaviour
{

    public GameObject bullet;
    public float timeBetweenShots = 0.11111f;

    private float timestamp;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Shoots Right
        if (Time.time >= timestamp && Input.GetButton("Fire4"))
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = 0.05f;
        }

        //Shoots Left
        else if (Time.time >= timestamp && Input.GetButton("Fire3"))
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = -0.05f;

        }
        //Shoots Up
        else if (Time.time >= timestamp && Input.GetButton("Fire1"))
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = 0.05f;

        }
        //Shoots Down
        else if (Time.time >= timestamp && Input.GetButton("Fire2"))
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = -0.05f;

        }

    }
}
