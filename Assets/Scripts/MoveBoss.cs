using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    public Transform player;
    public Transform myTransform;
    public float maxSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * maxSpeed * Time.deltaTime);
    }
}
