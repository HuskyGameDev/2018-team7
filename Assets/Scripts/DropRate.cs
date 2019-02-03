using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRate : MonoBehaviour
{
	private bool doSpawn = false;
	private int gunToSpawn;

	public void SetWeaponDrop()
	{
		int decide = Random.Range(0, 4);
		if (decide == 1)
		{
			doSpawn = true;
			gunToSpawn = Random.Range(1, 101);
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
			if (gunToSpawn >= 1 && gunToSpawn < 50)
			{
				int temp = GetGun("Shotgun");
				forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
				forNaming.name = guns[temp].name;
				return forNaming;
			}
			else if (gunToSpawn >= 50 && gunToSpawn < 70)
			{
				int temp = GetGun("SMG");
				forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
				forNaming.name = guns[temp].name;
				return forNaming;
			}
			else if (gunToSpawn >= 70 && gunToSpawn < 95)
			{
				int temp = GetGun("Sniper");
				forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
				forNaming.name = guns[temp].name;
				return forNaming;
			}
			else if (gunToSpawn >= 95 && gunToSpawn < 100)
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
