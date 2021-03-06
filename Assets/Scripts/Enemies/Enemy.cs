﻿using UnityEngine;
using System.Collections;

public enum EnemyType
{
	Helicopter, Sentry, Bomber, Patrol, AssaultPatrol, ShotgunPatrol, Tank, Boss
}

public class Enemy : MonoBehaviour
{
	public int health;
	public Room room;

	protected SpriteRenderer rend;
	public PlayerController pc { get; private set; }

	private Color baseColor;

	public Vector2 Pos
	{
		get { return transform.position; }
	}

	private void Awake()
	{
		rend = GetComponent<SpriteRenderer>();
		baseColor = rend.color;

		GameObject player = GameObject.FindWithTag("Player");

		if (player != null)
			pc = player.GetComponent<PlayerController>();
	}

	public void ApplyDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			SpawnWeapon(transform.position);
			Destroy(gameObject);
		}
		else StartCoroutine(TintRed());
	}

	// Spawns a new weapon with at a 25% chance at the Vector3 position
	public void SpawnWeapon(Vector3 pos)
	{
		EnemyLoot loot = GetComponent<EnemyLoot>();

		if (loot != null)
			loot.SpawnGun(pos.x, pos.y);
	}

	private IEnumerator TintRed()
	{
		if (rend != null)
		{
			rend.color = Color.red;
			yield return new WaitForSeconds(0.1f);
			rend.color = baseColor;
		}
	}
}
