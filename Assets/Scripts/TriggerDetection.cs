using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
	private PlayerController pc;

	void Start()
	{
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

	void OnTriggerStay(Collider other)
	{
		Transform parent = other.transform.parent;

		if (parent != null && parent.CompareTag("Player"))
		{
			Vector3 dir = (other.transform.position - transform.position).normalized;
			pc.ApplyDamage(10, dir, 25.0f);
		}
	}
}
