using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum Facing
{
	Right, Left, Front, Back
}

public class PlayerController : MonoBehaviour
{
	// The player's current gun.
	public int Gun { get; private set; }

	public bool Dead { get; set; } = false;

	public Facing FacingDir { get; private set; }

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
			_health = Mathf.Clamp(_health, 0, 100);

			if (_health == 0)
			{
				gameObject.SetActive(false);
				Dead = true;
				Invoke("LoadGameOver", 3.0f);
			}
		}
	}

	// Stores guns the player has. An entry will be null if the player hasn't
	// unlocked that gun yet.
	private Gun[] guns = new Gun[GunType.Count];

	private SpriteRenderer rend;
	public Sprite[] sprites;

	public AudioSource GetGunAudioSource()
	{
		return transform.Find("Gun Audio Source").GetComponent<AudioSource>();
	}

	public float getBulletSpeed()
	{
		return guns[Gun].speed;
	}
    
	public void setBulletSpeed(float speed)
	{
		guns[Gun].speed = speed;
	}

	private IEnumerator TintRed()
	{
		rend.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		rend.color = Color.white;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;

		if (!Dead)
			StartCoroutine(TintRed());
	}

	/**
     * Start
     * Initializes the health and bulletSpeed variables
     */
	void Start()
	{
		rend = GetComponent<SpriteRenderer>();
		controller = GetComponent<CharacterController>();

		health = 100;

		AddGun<Pistol>(GunType.Pistol);
		Gun = GunType.Pistol;
	}

	public void ChangeFacing(Facing facing)
	{
		FacingDir = facing;
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

	private void ChangeGun(int type)
	{
		if (HasGun(type))
		{
			Gun = type;
			guns[type].Activate(this);
		}
	}

	public void AddGun<T>(int type) where T: Gun, new()
	{
		T gun = new T();
		gun.Init(this);
		guns[type] = gun;
	}

	public bool HasGun(int type)
	{
		return guns[type] != null;
	}

	private void Update()
	{
		if (Time.timeScale == 0.0f)
			return;

		if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeGun(GunType.Pistol);
		if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeGun(GunType.Shotgun);
		if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeGun(GunType.SMG);
		if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeGun(GunType.Sniper);
		if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeGun(GunType.Minigun);

		room = Floor.Instance.GetRoom(Utils.ToRoomPos(transform.position));
		room.ActivateEnemies();

		guns[Gun].Fire(this);
	}

	private void LoadGameOver()
	{
		SceneManager.LoadScene("GameOver");
	}
}
