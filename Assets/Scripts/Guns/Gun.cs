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
	protected GameObject bulletPrefab;
	protected PlayerController pc;
	protected AudioSource audioSource;
	[HideInInspector] public float speed; // the speed at which the player shoots

	private Queue<BulletController> bulletPool = new Queue<BulletController>();

	protected BulletController CreateBullet(Transform t)
	{
		BulletController bullet = GetBullet();
		bullet.transform.position = t.position;
		bullet.ChangeFacing(pc.FacingDir);
		Physics.IgnoreCollision(bullet.GetComponent<BoxCollider>(), t.GetComponentInChildren<BoxCollider>());
		return bullet;
	}

	protected BulletController GetBullet()
	{
		BulletController bullet;

		if (bulletPool.Count > 0)
		{
			bullet = bulletPool.Dequeue();
			bullet.gameObject.SetActive(true);
		}
		else
		{
			bullet = Object.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity).GetComponent<BulletController>();
			bullet.GetComponent<Rigidbody>().isKinematic = false;
			bullet.gun = this;
		}

		bullet.OnFired();
		return bullet;
	}

	public void ReturnBullet(BulletController obj)
	{
		obj.gameObject.SetActive(false);
		bulletPool.Enqueue(obj);
	}

	public void Init(PlayerController pc)
	{
		this.pc = pc;
		audioSource = pc.GetGunAudioSource();
		bulletPrefab = Resources.Load<GameObject>("Bullet");
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
