using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupSpec
{
	public GameObject obj;

	[Range(0, 100)]
	public int chance;
}

public class ItemSpawner : MonoBehaviour
{
	public PickupSpec[] items;
    int whatToSpawn;

    public void SpawnItem(Vector2 pos)
	{
		float r = Random.Range(0, 100);

		for (int i = 0; i < items.Length; i++)
		{
			PickupSpec spec = items[i];

			if (r < spec.chance)
			{
				GameObject obj = Instantiate(spec.obj, new Vector3(pos.x, pos.y, -1.0f), Quaternion.identity);
				obj.name = spec.obj.name;
				break;
			}
		}
    }
	
	// Update is called once per frame
	//void Update () {

        //if (Time.time > nextSpawn)
        //{



        //    whatToSpawn = Random.Range(1, 3); //Change this to be 1+ the number of items
        //    Debug.Log(whatToSpawn);

        //    switch (whatToSpawn)
        //    {
        //        case 1:
        //            forNaming = (GameObject)Instantiate(prefab1, transform.position, Quaternion.identity);
        //            forNaming.name = prefab1.name;
        //            break;
        //        case 2:
        //            forNaming = (GameObject)Instantiate(prefab2, transform.position, Quaternion.identity);
        //            forNaming.name = prefab2.name;
        //            break;
        //    }

        //    nextSpawn = Time.time + spawnRate;


        //}

    //}
}
