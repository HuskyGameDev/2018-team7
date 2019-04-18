using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    private PlayerController pc;
	private float range = 1.0f;
    private Transform target;
	private ParticleSystem ps;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<Enemy>().pc;
		ps = GetComponent<ParticleSystem>();
        sound = GetComponent<AudioSource>();

		if (pc != null)
			target = pc.transform;
    }

    // Update is called once per frame
    void Update()
	{
		if (target == null)
			return;

        if (Vector3.Distance(transform.position, target.position) <= range)
            Explode();
    }

    private void Explode()
    {
		Vector3 dir = (target.position - transform.position).normalized;
		pc.ApplyDamage(50, dir, 50.0f);

		// Disable the visual rendering and the running of this script.
		// We do this since we can't destroy the enemy until the particles finish playing.
		GetComponent<SpriteRenderer>().enabled = false;
		enabled = false;

		ps.Play();
        sound.Play();

		// Destroy after the particles have fully played out
		// to prevent them from being cut off.
		Destroy(gameObject, sound.clip.length);
    }
}
