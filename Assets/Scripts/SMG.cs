using UnityEngine;

public class SMG : Gun
{
    public GameObject bullet;
    public float timeBetweenShots = 0.11111f;
    public int pelletCount;
    public float spreadAngle;
    public float pelletFireVel = 1;
    //public GameObject pellet;
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
    void Update() {
        if (Time.time >= timestamp && Input.GetKey(KeyCode.RightArrow))
        {
            Fire();
        }
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.LeftArrow))
        {
            Fire();
        }
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.UpArrow))
        {
            Fire();
        }
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.DownArrow))
        {
            Fire();
        }
    }

      void Fire()
    {
        int i = 0;
        //Shoots Right
        if (Time.time >= timestamp && Input.GetKey(KeyCode.RightArrow))
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

        //Shoots Left
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.LeftArrow))
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
        //Shoots Up
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.UpArrow))
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
        //Shoots Down
        else if (Time.time >= timestamp && Input.GetKey(KeyCode.DownArrow))
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

    }
}
