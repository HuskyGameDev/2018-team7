using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float speed;

	private CharacterController controller;
	private SpriteRenderer rend;

	private Transform player; // Used to see where the player is.

	// Separate from 'player' so we can make this null if the enemy has no target.
	private Transform target;

	private int health = 10;

	// Use this for initialization
	void Start()
    {
        //StartCoroutine(WaitTime(2));
        speed = 2.5f;

		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();

		player = GameObject.FindWithTag("Player").transform;

		transform.SetZ(-0.1f);
    }

    // Update is called once per frame
    // Changes the direction of the enemy's movement towards the player once per frame
    void Update()
    {
		float dist = Vector3.Distance(transform.position, player.position);

		if (dist <= 5.0f)
			target = player;

		if (target != null)
		{
			// if away from the player move towards him/her
			if (dist > 1f)
			{
				Vector3 pcDirection = (target.position - transform.position).normalized;
				controller.Move(pcDirection * speed * Time.deltaTime);
				transform.SetZ(-.1f);
			}

			// if on top of the player, slow down
			if (Mathf.Approximately(dist, 0.0f))
			{
				speed -= 2f;
				StartCoroutine(WaitTime(1));
				speed += 2f;
			}
		}
    }

    IEnumerator WaitTime(int timer)
    {
        yield return new WaitForSeconds(timer);
    }

	public void ApplyDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
			Destroy(gameObject);
		else StartCoroutine(TintRed());
	}

	private IEnumerator TintRed()
	{
		rend.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		rend.color = Color.white;
	}
}