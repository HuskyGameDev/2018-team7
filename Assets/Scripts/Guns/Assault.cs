using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if false
public class Assault : MonoBehaviour
{

    public GameObject bullet;
    

    // Use this for initialization
    void Start()
    {


    }


    //NOT FINAL
    // Update is called once per frame
    void Update()
    {

		//Shoots Right
		if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = 0.05f;

            GameObject go1 = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go1.GetComponent<BulletController>().speedX = 0.055f;

            GameObject go2 = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go2.GetComponent<BulletController>().speedX = 0.06f;

        }

        //Shoots Left
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = -0.05f;

            GameObject go1 = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go1.GetComponent<BulletController>().speedX = -0.055f;

            GameObject go2 = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go2.GetComponent<BulletController>().speedX = -0.06f;
        }
        //Shoots Up
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = 0.05f;

            GameObject go1 = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go1.GetComponent<BulletController>().speedY = 0.055f;

            GameObject go2 = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go2.GetComponent<BulletController>().speedY = 0.06f;
        }
        //Shoots Down
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = -0.05f;

            GameObject go1 = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go1.GetComponent<BulletController>().speedY = -0.055f;

            GameObject go2 = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go2.GetComponent<BulletController>().speedY = -0.06f;
        }
    }
}
#endif
