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

	public float FireRateModifier { get; private set; } = 1.0f;

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

	// The health of the player
	// This should not be set directly outside of this class. 
	// Use ApplyDamage() and Heal() instead.
	public int health
	{
		get { return _health; }
		private set
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

    public Gun GetCurrentGun()
    {
        return guns[Gun];
    }

	private SpriteRenderer rend;
	public Sprite[] sprites;

	public AudioSource GetGunAudioSource()
	{
		return transform.Find("Gun Audio Source").GetComponent<AudioSource>();
	}
    
	public void SetFireRateModifier(float modifier)
	{
		FireRateModifier = modifier;
	}

	private IEnumerator TintRed()
	{
		rend.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		rend.color = Color.white;
	}

	public void ApplyDamage(int damage, Vector3 knockbackDir = default(Vector3), float knockbackForce = 0.0f)
	{
		health -= damage;

		if (!Dead)
		{
			StartCoroutine(TintRed());
			GetComponent<Move>().ApplyKnockback(knockbackDir, knockbackForce);
		}
	}

	public void Heal(int amount)
	{
		health += amount;
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

	private void AddGun<T>(int type) where T: Gun, new()
	{
		T gun = new T();
		gun.Init(this);
		guns[type] = gun;
	}

    public void AddGun(int type)
    {
		switch (type)
		{
			case GunType.Pistol:
				AddGun<Pistol>(GunType.Pistol);
				break;

			case GunType.Shotgun:
				AddGun<Shotgun>(GunType.Shotgun);
				break;

			case GunType.SMG:
				AddGun<SMG>(GunType.SMG);
				break;

			case GunType.Sniper:
				AddGun<Sniper>(GunType.Sniper);
				break;

			case GunType.Minigun:
				AddGun<Minigun>(GunType.Minigun);
				break;

			case GunType.Klusterfunk:
				AddGun<Klusterfunk>(GunType.Klusterfunk);
				break;

			case GunType.Laser:
				AddGun<Laser>(GunType.Laser);
				break;
		}
    }
   
	public bool HasGun(int type)
	{
		return guns[type] != null;
	}

	private void ChangeGun(int k)
	{
		int count = -1;

		// Guns are stored at fixed locations in the guns array, but the player
		// may not have them all. This code maps input in such a way to where, 
		// for example, if the player has the pistol (id 0) and SMG (id 3), we have:
		// [Pistol] [ ] [SMG] [ ] [ ] [ ] ... 
		// If we simply map the number key pressed to this array, then 2 would lead to that 
		// empty spot and not select a gun. We want it to map to SMG, since SMG is the second
		// gun the player has. That's what the code below does.
		for (int i = 0; i < guns.Length; i++)
		{
			if (guns[i] != null)
				count++;

			if (count == k)
			{
				Gun = i;
				guns[i].Activate(this);
				return;
			}
		}
	}

	private void GetNextGun()
	{
		do
		{
			Gun = (Gun + 1) % guns.Length;
		}
		while (guns[Gun] == null);
	}

	private void GetPrevGun()
	{
		do
		{
			Gun--;
			if (Gun < 0) Gun = guns.Length - 1;
		}
		while (guns[Gun] == null);
	}

	private void Update()
	{
		if (Time.timeScale == 0.0f)
			return;

		if (Input.GetKeyDown(KeyCode.E))
		{
			GetNextGun();
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			GetPrevGun();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeGun(0);
		if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeGun(1);
		if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeGun(2);
		if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeGun(3);
		if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeGun(4);
		if (Input.GetKeyDown(KeyCode.Alpha6)) ChangeGun(5);

		room = Floor.Instance.GetRoom(Utils.ToRoomPos(transform.position));
		room.ActivateEnemies();

		guns[Gun].Fire(this);
	}

    public bool[] GetGunArray()
    {
        bool[] ret = new bool[GunType.Count];
        for(int i = 0; i < GunType.Count; i++)
        {
            ret[i] = HasGun(i);
        }
        return ret;
    }

    //this breaks once new guns are added, be wary
    public void SetGunArray(bool[] gunArray)
    {
        for (int i = 0; i < GunType.Count; i++)
        {
            if (gunArray[i])
            {
                AddGun(i);
            }
        }
    }

	private void LoadGameOver()
	{
		SceneManager.LoadScene("GameOver");
	}
}
