using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Vector3 velocity;
	private PlayerController pc;

	// The gun this bullet belongs to.
	public Gun gun { get; set; }
	public BulletPool Pool { get; set; }

	public bool rotate, bounce;

	// Use this for initialization
	void Start()
	{
		// retrieve the player object
		GameObject playerControllerObject = GameObject.FindWithTag("Player");
		if (playerControllerObject != null)
		{
			pc = playerControllerObject.GetComponent<PlayerController>();
		}
		else
		{
			Debug.Log("Cannot find Player");
		}
	}

	public void OnFired(float duration)
	{
		StartCoroutine(DestroyBullet(duration));
	}

	public void SetSpeed(float speed)
	{
		velocity = Vector3.right * speed;
	}

	// Update is called once per frame
	void Update()
	{
		// Only rotate on z.
		Vector3 rot = transform.rotation.eulerAngles;

		if (rotate)
			rot.z += 50.0f * Time.deltaTime;

		transform.rotation = Quaternion.Euler(0.0f, 0.0f, rot.z);

		transform.Translate(velocity * Time.deltaTime, Space.Self);
		transform.SetZ(-1.0f);
	}

	public void ChangeFacing(Facing facing)
	{
		switch (facing)
		{
			case Facing.Back:
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
				break;

			case Facing.Front:
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
				break;

			case Facing.Right:
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
				break;

			case Facing.Left:
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
				break;
		}
	}

	IEnumerator DestroyBullet(float duration)
	{
		yield return new WaitForSeconds(duration);
		Pool.ReturnBullet(this);
	}

	private void OnTriggerEnter(Collider other)
	{
		Vector3 dir = (other.transform.position - transform.position).normalized;

		// 8 - player layer, 9 - enemy layer.
		if (other.gameObject.layer == 9)
		{
			other.GetComponentInParent<Enemy>().ApplyDamage(gun.damage);

			// if the weapon is a sniper, don't destroy
			if (pc.Gun == GunType.Sniper)
				return;
		}
		else if (other.gameObject.layer == 8)
			pc.ApplyDamage(10, dir, 25.0f);

		if (bounce)
		{
			RaycastHit hit;
			Ray ray = new Ray(other.ClosestPointOnBounds(transform.position) + (-dir * 0.05f), dir);
			Physics.Raycast(ray, out hit);
			Vector3 reflect = Vector3.Reflect(dir, hit.normal);
			Vector3 rot = Vector3.RotateTowards(dir, reflect, Mathf.PI * 2, 0.0f);
			transform.rotation = Utils.LookX(rot);
		}
		else Pool.ReturnBullet(this);
	}
}
