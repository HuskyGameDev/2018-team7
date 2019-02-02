using UnityEngine;
using UnityEngine.SceneManagement;

public enum Facing
{
	Right, Left, Front, Back
}

public class PlayerController : MonoBehaviour
{
	// The player's current gun.
	private Gun gun;

	public bool Dead { get; set; } = false;

	private CharacterController controller;

	/// <summary>
	/// Returns the position at the player's feet. The character controller is centered around the area
	/// we wish to return and is specified using an offset from the transform position.
	/// </summary>
	public Vector2 FeetPosition
	{
		get { return transform.position + controller.center; }
	}

	// The room the player is currently in.
	private Room room;

	private int _health;

	// the health of the player
	public int health
	{
		get { return _health; }
		set
		{
			_health = value;
			if (_health <= 0)
			{
				gameObject.SetActive(false);
				Dead = true;
				Invoke("LoadGameOver", 3.0f);
			}
		}
	}

    // Gun enablers
    public bool shotgun { get; set; }
    public bool smg { get; set; }
    public bool sniper { get; set; }
    public bool minigun { get; set; }

	private SpriteRenderer rend;
	public Sprite[] sprites;

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

		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();
	}

	public void ChangeFacing(Facing facing)
	{
		rend.sprite = sprites[(int)facing];
	}

	public void UpdateSprite(Vector2 accel)
	{
		if (accel.x > 0.5f)
			ChangeFacing(Facing.Right);
		else if (accel.x < -0.5f)
			ChangeFacing(Facing.Left);
		else if (accel.y < -0.5f)
			ChangeFacing(Facing.Front);
		else if (accel.y > 0.5f)
			ChangeFacing(Facing.Back);
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
		if (Time.timeScale == 0.0f)
			return;

		if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeGun<Pistol>();
		if (Input.GetKeyDown(KeyCode.Alpha2) && shotgun) ChangeGun<Shotgun>();
		if (Input.GetKeyDown(KeyCode.Alpha3) && smg) ChangeGun<SMG>();
		if (Input.GetKeyDown(KeyCode.Alpha4) && sniper) ChangeGun<Sniper>();
		if (Input.GetKeyDown(KeyCode.Alpha5) && minigun) ChangeGun<Minigun>();

		room = Floor.Instance.GetRoom(Utils.ToRoomPos(transform.position));
		room.ActivateEnemies();

		gun.CheckFire();
	}

	private void LoadGameOver()
	{
		SceneManager.LoadScene("GameOver");
	}
}
