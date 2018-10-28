using UnityEngine;

public class Minigun : Gun
{
    public float timeBetweenShots = 0.05f;

    private float timestamp;

	protected override void Init()
	{
		speed = 0.2f;
	}

	// Update is called once per frame
	void Update () {

        //Shoots Right
        if (Time.time >= timestamp && Input.GetKey(KeyCode.RightArrow))
        {
            timestamp = Time.time + timeBetweenShots;
            

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = 0.05f;
        }

        //Shoots Left
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.LeftArrow))
        {
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = -0.05f;

        }
        //Shoots Up
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.UpArrow))
        {
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = 0.05f;

        }
        //Shoots Down
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.DownArrow))
        {
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = -0.05f;

        }
		
	}
}
