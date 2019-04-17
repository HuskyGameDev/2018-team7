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

	public Sprite[] sprites;

	private BulletPool bullets = new BulletPool();

	public GameObject explosion;

	private Enemy enemy;

	private float fireRate;
	private float timeBeforeFire;

	private float reflectRate = 0.3f;
	private float timeBeforeReflect;

	private float maxHealth;

	private List<GameObject> bombers = new List<GameObject>();

	private Room room;

	private float spiralStartRot = 0.0f;

	void Start()
    {
		enemy = GetComponent<Enemy>();
		maxHealth = enemy.health;
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StartAfterDelay());
	}

	void Update()
	{
		timeBeforeReflect -= Time.deltaTime;
	
		if (enemy.health <= 0.0f)
		{
			GetComponent<SpriteRenderer>().color = Color.gray;
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
    }

	public void CreatePhase1Spots(Room room)
	{
		this.room = room;
		Vector2 wPos = room.WorldPos;

		Transform spot1 = new GameObject("Spot1").GetComponent<Transform>();
		Transform spot2 = new GameObject("Spot2").GetComponent<Transform>();
		Transform spot3 = new GameObject("Spot3").GetComponent<Transform>();

		spot1.position = wPos + new Vector2(5.0f, 4.0f);
		spot2.position = wPos + new Vector2(Room.Width / 2.0f, Room.Height - 4.0f);
		spot3.position = wPos + new Vector2(Room.Width - 4.0f, 4.0f);

		spots = new Transform[] { spot1, spot2, spot3 };
	}

	public void CreatePhase3Spots()
	{
		Vector2 wPos = room.WorldPos;

		Transform spot1 = new GameObject("Spot1").GetComponent<Transform>();
		Transform spot2 = new GameObject("Spot2").GetComponent<Transform>();
		Transform spot3 = new GameObject("Spot3").GetComponent<Transform>();
		Transform spot4 = new GameObject("Spot4").GetComponent<Transform>();
		Transform spot5 = new GameObject("Spot5").GetComponent<Transform>();

		spot1.position = wPos + new Vector2(Room.Width / 2, Room.Height / 2);
		spot2.position = wPos + new Vector2(Room.Width / 2, Room.Height - 2.0f);
		spot3.position = wPos + new Vector2(Room.Width - 4.0f, Room.Height / 2);
		spot4.position = wPos + new Vector2(Room.Width / 2, 3.0f);
		spot5.position = wPos + new Vector2(3.0f, Room.Height / 2);

		for (int i = 0; i < spots.Length; i++)
			Destroy(spots[i].gameObject);

		spots = new Transform[] { spot1, spot2, spot3, spot4, spot5 };
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

	private void SpiralShot()
	{
		spiralStartRot -= 15.0f * Time.deltaTime;
		timeBeforeFire -= Time.deltaTime;

		if (timeBeforeFire <= 0.0f)
		{
			for (int i = 0; i < 8; i++)
			{
				timeBeforeFire = fireRate;
				Bullet bullet = bullets.CreateBullet(transform, transform, 25.0f);
				bullet.rotate = true;
				bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, spiralStartRot + (i * 45));
				bullet.SetSpeed(15.0f);
				bullet.gameObject.layer = 15;
			}
		}
	}

	private void ReflectShot()
	{
		if (timeBeforeReflect < 0.0f)
		{
			timeBeforeReflect = reflectRate;
			Bullet bullet = bullets.CreateBullet(transform, transform, 3.0f);
			bullet.bounce = true;
			bullet.SetSpeed(15.0f);
			bullet.gameObject.layer = 15;
		}
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
		StartCoroutine(CheckPhase2());
		StartCoroutine(Phase1());
	}

	IEnumerator CheckPhase2()
	{
		while (enemy.health > (maxHealth * 0.6f))
			yield return null;

		StopAllCoroutines();
		StartCoroutine(CheckPhase3());
		StartCoroutine(Phase2());
	}

	IEnumerator CheckPhase3()
	{
		while (enemy.health > (maxHealth * 0.3f))
			yield return null;

		StopAllCoroutines();
		StartCoroutine(Phase3());
	}

    IEnumerator Phase1()
    {
		GetComponent<SpriteRenderer>().sprite = sprites[0];

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
		GetComponent<SpriteRenderer>().sprite = sprites[1];

		fireRate = 0.15f;
		speed = 3.0f;

		float bomberDelay = 2.0f;
		float bomberTimeLeft = bomberDelay;

		while (true)
		{
			timeBeforeFire -= Time.deltaTime;
			bomberTimeLeft -= Time.deltaTime;

			if (bomberTimeLeft < 0.0f)
			{
				SummonBomber();
				bomberTimeLeft = bomberDelay;
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

	IEnumerator Phase3()
	{
		GetComponent<SpriteRenderer>().sprite = sprites[2];

		speed = 5.0f;
		fireRate = 0.15f;
		CreatePhase3Spots();

		while (true)
		{
			while (transform.position.x != spots[0].position.x)
			{
				ReflectShot();
				transform.position = Vector2.MoveTowards(transform.position, spots[0].position, speed * Time.deltaTime);
				yield return null;
			}

			yield return StartCoroutine(FireSpirals());

			while (transform.position != spots[1].position)
			{
				ReflectShot();
				transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed * Time.deltaTime);
				yield return null;
			}

			while (transform.position != spots[2].position)
			{
				ReflectShot();
				transform.position = Vector2.MoveTowards(transform.position, spots[2].position, speed * Time.deltaTime);
				yield return null;
			}

			while (transform.position != spots[3].position)
			{
				ReflectShot();
				transform.position = Vector2.MoveTowards(transform.position, spots[3].position, speed * Time.deltaTime);
				yield return null;
			}

			while (transform.position != spots[4].position)
			{
				ReflectShot();
				transform.position = Vector2.MoveTowards(transform.position, spots[4].position, speed * Time.deltaTime);
				yield return null;
			}
		}
	}

	IEnumerator FireSpirals()
	{
		float time = 6.0f;

		while (time > 0.0f)
		{
			time -= Time.deltaTime;
			SpiralShot();
			yield return null;
		}
	}
}
