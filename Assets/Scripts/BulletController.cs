using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speedX = 0f;
    public float speedY = 0f;
    private PlayerController playerController;

    // Use this for initialization
    void Start ()
	{
        // retrieve the player object
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Cannot find Player");
        }

        StartCoroutine(DestroyBullet());
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Only rotate on z.
		Vector3 rot = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, rot.z);

		transform.Translate(new Vector3(speedX, speedY) * Time.deltaTime, Space.Self);
		transform.SetZ(-1.0f);
	}

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider other)
	{
        // Enemy layer.
        if (other.gameObject.layer == 9)
        {
            other.GetComponentInParent<EnemyController>().ApplyDamage(4);

            // if the weapon is a sniper, don't destroy
            if (playerController.sniper)
                return;
        }    
        Destroy(gameObject);
	}
}
