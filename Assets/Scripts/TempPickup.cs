using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPickup : MonoBehaviour
{
    public GameObject effect;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Pickup(collision);
        }
    }

    void Pickup(Collider player)
    {
        Instantiate(effect, transform.position, transform.rotation);

        Destroy(effect);
    }

}
