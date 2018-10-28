using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speedX = 0f;
    public float speedY = 0f;

    // NOT FINAL
    // Use this for initialization
    void Start () {
        StartCoroutine(DestroyBullet());
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Only rotate on z. This is a little hacky.
		Vector3 rot = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, rot.z);

		transform.Translate(new Vector3(speedX, speedY), Space.Self);
	}

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider other)
	{
		// Default layer - walls.
		if (other.gameObject.layer == 0)
			Destroy(gameObject);
	}
}
