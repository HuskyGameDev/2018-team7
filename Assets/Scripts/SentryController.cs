using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryController : MonoBehaviour
{
    private PlayerController pc;
    public Transform target;
    private float range = 20f;
    private int health { get; set; }
    private Gun gun;
    private float timeBetweenShots = 1.0f;
    private float timeStamp;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        health = 25;
    }

    // Update is called once per frame
    void Update()
    {
        // If player is in range, shoot in direction
        if (Vector3.Distance(transform.position, target.position) <= range)
        {
            Shoot(transform, target);
        }
    }

    // Applies damage taken by the player to the enemy
    void ApplyDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Shoots a bullet in the direction of the target
    void Shoot(Transform transform, Transform target)
    {
        if (Time.time > timeStamp)
        {
            timeStamp = Time.time + timeBetweenShots;
            transform.LookAt(target);
            GameObject go = CreateBullet(transform);
        }
    }

    // creates a bullet based off of the Gun class
    // should be integrated another way eventually but works for now
    protected GameObject CreateBullet(Transform t, Quaternion rot = default(Quaternion))
    {
        GameObject go = Instantiate(bullet, t.position, rot);
        Physics.IgnoreCollision(go.GetComponent<BoxCollider>(), t.GetComponentInChildren<BoxCollider>());
        return go;
    }

}
