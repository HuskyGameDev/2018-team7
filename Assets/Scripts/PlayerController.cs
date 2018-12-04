using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// The player's current gun.
	private Gun gun;

	public int health { get; set; } // the health of the player

    // Gun enablers
    public bool shotgun { get; set; }
    public bool smg { get; set; }
    public bool sniper { get; set; }
    public bool minigun { get; set; }

	public float getBulletSpeed()
	{
		return gun.speed;
	}
    
	public void setBulletSpeed(float speed)
	{
		gun.speed = speed;
	}

	/**
     * Start
     * Initializes the health and bulletSpeed variables
     */
	void Start()
	{
		health = 100;
		gun = GetComponent<Gun>();
        shotgun = false;
        smg = false;
        sniper = false;
        minigun = false;
	}

	// Changes the gun type to the type specified by T.
	private void ChangeGun<T>() where T: Gun
	{
		Destroy(gun);
		gun = gameObject.AddComponent<T>();
		Debug.Log("Gun changed to " + typeof(T).FullName);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeGun<Pistol>();
		if (Input.GetKeyDown(KeyCode.Alpha2) && shotgun) ChangeGun<Shotgun>();
		if (Input.GetKeyDown(KeyCode.Alpha3) && smg) ChangeGun<SMG>();
		if (Input.GetKeyDown(KeyCode.Alpha4) && sniper) ChangeGun<Sniper>();
		if (Input.GetKeyDown(KeyCode.Alpha5) && minigun) ChangeGun<Minigun>();
	}
}
