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
		int decide = Random.Range(0, 5);
		if (decide == 1)
		{
			doSpawn = true;
			gunToSpawn = Random.value;
		}
	}

	public GameObject[] guns;

	private GameObject SpawnPickup(string name, float x, float y)
	{
		int temp = GetGun(name);
		GameObject forNaming = Instantiate(guns[temp], new Vector3(x, y, -1.0f), Quaternion.identity);
		forNaming.name = guns[temp].name;
		return forNaming;
	}

	 // Spawns a new gun based off of the comment above at x, y
	 public GameObject SpawnGun(float x, float y)
	 {
		if (doSpawn)
		{
			gunToSpawn = 0.95f;

			if (gunToSpawn < 0.02f) return SpawnPickup("Klusterfunk", x, y);
			else if (gunToSpawn < 0.2f) return SpawnPickup("Shotgun", x, y);
			else if (gunToSpawn < 0.7f) return SpawnPickup("SMG", x, y);
			else if (gunToSpawn < 0.94f) return SpawnPickup("Sniper", x, y);
			else if (gunToSpawn < 0.97f) return SpawnPickup("Laser", x, y);
			else return SpawnPickup("Minigun", x, y);
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
