using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour {

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

    void OnTriggerEnter(Collider other)
    {
		Transform parent = other.transform.parent;

		if (parent != null && parent.CompareTag("Player"))
		{
			pc.TakeDamage(10);
			Vector3 dir = (other.transform.position - transform.position).normalized;
			parent.GetComponent<Move>().ApplyKnockback(dir, 25.0f);
		}
    }
}
