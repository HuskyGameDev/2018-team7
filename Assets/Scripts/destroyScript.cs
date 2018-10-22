using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyScript : MonoBehaviour {
   

   

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyBullet());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
