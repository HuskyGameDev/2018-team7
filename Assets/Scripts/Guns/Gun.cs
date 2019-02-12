using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Maps integer IDs to gun types.
/// </summary>
public static class GunType
{
	public const int Pistol = 0, Shotgun = 1, SMG = 2, Sniper = 3, Minigun = 4, Count = 5;
}

public class Gun
{
	protected GameObject bullet;
	protected PlayerController pc;
	protected AudioSource audioSource;
	[HideInInspector] public float speed; // the speed at which the player shoots

	protected GameObject CreateBullet(Transform t, Quaternion rot = default(Quaternion))
	{
		GameObject go = GameObject.Instantiate(bullet, t.position, rot);
		Physics.IgnoreCollision(go.GetComponent<BoxCollider>(), t.GetComponentInChildren<BoxCollider>());
		return go;
	}

	public void Init(PlayerController pc)
	{
		this.pc = pc;
		audioSource = pc.GetGunAudioSource();
		bullet = Resources.Load<GameObject>("Bullet");
		Start();
	}

	protected virtual void Start()
	{
		speed = 15.0f;
	}

	public virtual void Activate(PlayerController pc)
	{
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Handgun");
	}

	// All gun firing should go through this virtual method. This allows us to, for example, block all gun
	// firing when the game is paused. If every gun implements its own update method, then we have to
	// duplicate the checking code for every single gun - not the best code design.
	public virtual void Fire(PlayerController pc) { }
}
