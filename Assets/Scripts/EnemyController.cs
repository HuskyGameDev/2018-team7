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
	public Room room;

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
		{
			SpawnWeapon(transform.position);
			Destroy(gameObject);
		}
			
		else StartCoroutine(TintRed());
	}

	private IEnumerator TintRed()
	{
		rend.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		rend.color = Color.white;
	}

	// Spawns a new weapon with at a 25% chance at the Vector3 position
	public void SpawnWeapon(Vector3 pos)
	{
		int decide = Random.Range(0, 4);
		if (decide == 1)
		{
			DropRate drop = GetComponent<DropRate>();
			drop.SpawnGun(pos.x, pos.y);
		}
	}
}