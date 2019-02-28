using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Maps integer IDs to gun types.
/// </summary>
public static class GunType
{
	public const int Pistol = 0, Shotgun = 1, SMG = 2, Sniper = 3, Minigun = 4, Count = 5;
}

public class Gun
{
	protected PlayerController pc;
	protected AudioSource audioSource;
	[HideInInspector] public float speed; // the speed at which the player shoots

	protected float fireRate = 0.0f;
	protected float timeBeforeFire = 0.0f;

	protected BulletPool bulletPool = new BulletPool();

	public void Init(PlayerController pc)
	{
		this.pc = pc;
		audioSource = pc.GetGunAudioSource();
		Start();
	}

	protected Bullet CreateBullet(Transform t)
	{
		Bullet bullet = bulletPool.CreateBullet(t);
		bullet.ChangeFacing(pc.FacingDir);
		bullet.gun = this;
		bullet.gameObject.layer = 11;
		return bullet;
	}

	protected virtual void Start()
	{
		fireRate = 0.25f;
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
