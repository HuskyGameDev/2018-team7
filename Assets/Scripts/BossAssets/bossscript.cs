using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bossscript : MonoBehaviour
{
    public Transform[] spots;
    public float speed;
    public GameObject projectile;
    GameObject Player;
	public Transform hole;
    Vector3 playerPos;
    public bool vulnerable;

	private BulletPool bullets = new BulletPool();

	public GameObject explosion;

	private Enemy enemy;

	private float fireRate;
	private float timeBeforeFire;

	private float maxHealth;

	private List<GameObject> bombers = new List<GameObject>();

	// Use this for initialization
	void Start()
    {
		enemy = GetComponent<Enemy>();
		maxHealth = enemy.health;
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StartAfterDelay());
	}

	void Update()
	{
		if (enemy.health <= (maxHealth * 0.5f))
		{
			StopAllCoroutines();
			StartCoroutine(Phase2());
		}

		if (enemy.health <= 0.0f)
		{
			GetComponent<SpriteRenderer>().color = Color.gray;
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
    }

	public void CreateSpots(Room room)
	{
		Vector2 wPos = room.WorldPos;

		Transform spot1 = new GameObject("Spot1").GetComponent<Transform>();
		Transform spot2 = new GameObject("Spot2").GetComponent<Transform>();
		Transform spot3 = new GameObject("Spot3").GetComponent<Transform>();

		spot1.position = wPos + new Vector2(5.0f, 4.0f);
		spot2.position = wPos + new Vector2(Room.Width / 2.0f, Room.Height - 4.0f);
		spot3.position = wPos + new Vector2(Room.Width - 4.0f, 4.0f);

		spots = new Transform[] { spot1, spot2, spot3 };
	}

	private void RandomBullet(float speed)
	{
		Quaternion quat = Random.rotation;
		Bullet p = bullets.CreateBullet(transform, transform, 25.0f);
		p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, quat, 360.0f);
		p.SetSpeed(speed);
		p.gameObject.layer = 15;
	}

	private void Burst()
	{
		for (int i = 50; i >= 0; i--)
			RandomBullet(25.0f);
	}

	private void TargetPlayer()
	{
		timeBeforeFire -= Time.deltaTime;

		if (timeBeforeFire <= 0.0f)
		{
			timeBeforeFire = fireRate;
			Bullet bullet = bullets.CreateBullet(transform, transform, 25.0f);
			Vector3 dir = (Player.transform.position - transform.position).normalized;
			bullet.transform.rotation = Utils.LookX(dir);
			bullet.SetSpeed(15.0f);
			bullet.gameObject.layer = 15;
		}
	}

	IEnumerator StartAfterDelay()
	{
		yield return new WaitForSeconds(1.0f);
		StartCoroutine(Phase2());
	}

    IEnumerator Phase1()
    {
		fireRate = 0.2f;
		speed = 8.0f;

		while (true)
        {
            while (transform.position.x != spots[0].position.x)
            {
				TargetPlayer();
                transform.position = Vector2.MoveTowards(transform.position, spots[0].position, speed * Time.deltaTime);

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
			Burst();
			yield return new WaitForSeconds(1.0f);
 
            while (transform.position.x != spots[2].position.x)
            {
				TargetPlayer();
				transform.position = Vector2.MoveTowards(transform.position, spots[2].position, speed * Time.deltaTime);
                yield return null;
            }

			yield return new WaitForSeconds(0.5f);
			Burst();
			yield return new WaitForSeconds(1.0f);
            
            while (transform.position.x != spots[1].position.x)
            {
				TargetPlayer();
				transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed * Time.deltaTime);
                yield return null;
            }

			yield return new WaitForSeconds(0.5f);
			Burst();
            yield return new WaitForSeconds(1.0f);
        }

    }

	private void SummonBomber()
	{
		GameObject[] enemyPrefabs = Floor.Instance.GetEnemyPrefabs();
		GameObject bomber = Instantiate(enemyPrefabs[(int)EnemyType.Bomber], transform.position, Quaternion.identity);
		bomber.SetActive(true);
		bomber.GetComponent<EnemyFollowAI>().SetSpeed(7.5f);
		bombers.Add(bomber);
	}

	IEnumerator Phase2()
	{
		fireRate = 0.06f;
		speed = 3.0f;

		float bomberDelay = 5.0f;

		while (true)
		{
			timeBeforeFire -= Time.deltaTime;
			bomberDelay -= Time.deltaTime;

			if (bomberDelay < 0.0f)
			{
				SummonBomber();
				bomberDelay = 5.0f;
			}

			if (timeBeforeFire <= 0.0f)
			{
				RandomBullet(5.0f);
				timeBeforeFire = fireRate;
			}

			transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
			yield return null;
		}
	}
}
