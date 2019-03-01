using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
	private bool doSpawn = false;
	private float gunToSpawn;

	/// <summary>
	/// Determines which weapon will drop, if any, for this entity. This is called in advance instead of deciding at the time the drop 
	/// is requested for seeding. If we generate random numbers at the time the entity is killed, then the player's choice in killing enemies in
	/// some order or skipping some (if allowed) would affect all the random numbers that generate after. So all generation of random numbers should
	/// happen once at floor load time.
	/// </summary>
	public void SetWeaponDrop()
	{
		int decide = Random.Range(0, 4);
		if (decide == 1)
		{
			doSpawn = true;
			gunToSpawn = Random.value;
		}
	}

	public GameObject[] guns;

	/**
		Drop Rates:
		Shotgun = 20%
		SMG = 50%
		Sniper = 25%
		Minigun = 5%
	 */
	 // Spawns a new gun based off of the comment above at x, y
	 public GameObject SpawnGun(float x, float y)
	 {
		if (doSpawn)
		{
			GameObject forNaming;

			if (gunToSpawn < 0.005f)
			{
				int temp = GetGun("Klusterfunk");
				forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
				forNaming.name = guns[temp].name;
				return forNaming;
			}
			else if (gunToSpawn < 0.5f)
			{
				int temp = GetGun("Shotgun");
				forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
				forNaming.name = guns[temp].name;
				return forNaming;
			}
			else if (gunToSpawn < 0.7f)
			{
				int temp = GetGun("SMG");
				forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
				forNaming.name = guns[temp].name;
				return forNaming;
			}
			else if (gunToSpawn < 0.95f)
			{
				int temp = GetGun("Sniper");
				forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
				forNaming.name = guns[temp].name;
				return forNaming;
			}
			else
			{
				int temp = GetGun("Minigun");
				forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
				forNaming.name = guns[temp].name;
				return forNaming;
			}
		}

		return null;
	 }

	/**
		Returns the index of where a gun is stored in the gun array
	 */
	 public int GetGun(string gunName)
	 {
		 for (int i = 0; i < guns.Length; i++)
		 {
			 if (guns[i].name.Equals(gunName))
			 	return i;
		 }
		 return -1;
	 }
}
