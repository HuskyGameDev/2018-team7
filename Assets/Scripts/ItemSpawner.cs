using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	public GameObject[] items;
    int whatToSpawn;

	// public float spawnRate = 2f;
    // float nextSpawn = 0f;

    GameObject forNaming;

    public void SpawnItem(Vector2 pos)
	{
        whatToSpawn = Random.Range(0, items.Length);
		forNaming = Instantiate(items[whatToSpawn], new Vector3(pos.x, pos.y, -1.0f), Quaternion.identity);
		forNaming.name = items[whatToSpawn].name;
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
