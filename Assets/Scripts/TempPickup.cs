using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPickup : MonoBehaviour
{
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Pickup(collision);
        }
    }

    void Pickup(Collider2D player)
    {
        Instantiate(effect, transform.position, transform.rotation);

        Destroy(effect);
    }

}
