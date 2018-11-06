using UnityEngine;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	protected GameObject bullet;
	protected PlayerController pc;
	[HideInInspector] public float speed; // the speed at which the player shoots
	public AudioSource audioSource;

	protected void Start()
	{
		pc = GetComponent<PlayerController>();
		audioSource = transform.Find("Gun Audio Source").GetComponent<AudioSource>();
		bullet = Resources.Load<GameObject>("Bullet");
		Init();
	}

	protected virtual void Init()
	{
		speed = 0.2f;
	}
}
