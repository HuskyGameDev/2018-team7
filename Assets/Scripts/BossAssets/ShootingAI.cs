using UnityEngine;
using System.Collections;

public class ShootingAI : MonoBehaviour {

    public Vector3 bulletOffset = new Vector3(0, .5f, 0);

    public GameObject bulletPrefab;

    public float fireDelay = 0.5f;
    float cooldownTimer = 0;

	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0)
        {
            Debug.Log("Enemy Pew!");
            cooldownTimer = fireDelay;

            Vector3 offset = transform.rotation * bulletOffset;

            GameObject bulletGO  = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
            bulletGO.layer = gameObject.layer;
        }
	
	}
}
