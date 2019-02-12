using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private Vector3 velocity;
	private PlayerController playerController;

	// The gun this bullet belongs to.
	public Gun gun { get; set; }

	// Use this for initialization
	void Start()
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
	}

	public void OnFired()
	{
		StartCoroutine(DestroyBullet());
	}

	public void SetVelocity(Vector3 vel)
	{
		velocity = vel;
	}

	// Update is called once per frame
	void Update()
	{
		// Only rotate on z.
		Vector3 rot = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, rot.z);

		transform.Translate(velocity * Time.deltaTime, Space.Self);
		transform.SetZ(-1.0f);
	}

	IEnumerator DestroyBullet()
	{
		yield return new WaitForSeconds(2f);
		gun.ReturnBullet(this);
	}

	private void OnTriggerEnter(Collider other)
	{
		// Enemy layer.
		if (other.gameObject.layer == 9)
		{
			other.GetComponentInParent<EnemyController>().ApplyDamage(4);

			// if the weapon is a sniper, don't destroy
			if (playerController.Gun == GunType.Sniper)
				return;
		}

		gun.ReturnBullet(this);
	}
}
