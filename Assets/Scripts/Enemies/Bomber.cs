using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    private PlayerController pc;
    private float range = 5f;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<Enemy>().pc;
        target = pc.transform;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= range)
            Explode();
    }

    private void Explode()
    {
        pc.health -= 50;
        Destroy(gameObject);
    }
}
