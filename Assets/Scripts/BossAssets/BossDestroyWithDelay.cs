using UnityEngine;
using System.Collections;

public class BossDestroyWithDelay : MonoBehaviour
{
    public float delay;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Block")
        {
            Destroy(gameObject);
        }
        
        
    }

    void Awake1()
    {
            Destroy(gameObject, delay);
        
    }
}
