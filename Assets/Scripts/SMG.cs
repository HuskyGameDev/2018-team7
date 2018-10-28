using UnityEngine;

public class SMG : Gun
{
    public float timeBetweenShots = 0.11111f;

    private float timestamp;

	protected override void Init()
	{
		speed = 0.1f;
	}

	// Update is called once per frame
	void Update()
    {

        //Shoots Right
        if (Time.time >= timestamp && Input.GetKey(KeyCode.RightArrow))
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;
            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = speed;
        }

        //Shoots Left
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.LeftArrow))
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedX = -speed;

        }
        //Shoots Up
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.UpArrow))
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = speed;

        }
        //Shoots Down
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.DownArrow))
        {
            //Instantiate(bullet, transform.position, transform.rotation);
            timestamp = Time.time + timeBetweenShots;

            GameObject go = (GameObject)Instantiate(bullet,
            transform.position, Quaternion.identity);
            go.GetComponent<BulletController>().speedY = -speed;

        }

    }
}
