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
    //public Sprite[] sprites;
    bool dead;

	private Enemy enemy;

	private float fireRate = 0.20f;
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
			dead = true;
			GetComponent<SpriteRenderer>().color = Color.gray;
			Instantiate(explosion, transform.position, Quaternion.identity);
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

	private void Burst()
	{
		for (int i = 50; i >= 0; i--)
		{
			Quaternion quat = Random.rotation;
			Bullet p = bullets.CreateBullet(transform, transform);
			p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, quat, 360.0f);
			p.SetSpeed(25.0f);
			p.gameObject.layer = 15;
		}
	}

	private void TargetPlayer()
	{
		timeBeforeFire -= Time.deltaTime;

		if (timeBeforeFire <= 0.0f)
		{
			timeBeforeFire = fireRate;
			Bullet bullet = bullets.CreateBullet(transform, transform);
			Vector3 dir = (Player.transform.position - transform.position).normalized;
			bullet.transform.rotation = Utils.LookX(dir);
			bullet.SetSpeed(15.0f);
			bullet.gameObject.layer = 15;
			bullet.OnFired();
		}
	}

	IEnumerator StartAfterDelay()
	{
		yield return new WaitForSeconds(1.0f);
		StartCoroutine(Phase2());
	}

    IEnumerator Phase1()
    {
		speed = 8.0f;

		while (true)
        {
            //FIRST ATTACK

            while (transform.position.x != spots[0].position.x)
            {
				TargetPlayer();

                //Only changes X Position
                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, transform.position.y), speed);
                transform.position = Vector2.MoveTowards(transform.position, spots[0].position, speed * Time.deltaTime);

                yield return null;
            }

            //transform.localScale = new   Vector2(-1, 1);

            //Delay before starting to shoot
            yield return new WaitForSeconds(.5f);

			Burst();
           
			yield return new WaitForSeconds(1f);
 
            //SECOND ATTACK
            //    GetComponent<Rigidbody2D>().isKinematic = true;
            while (transform.position.x != spots[2].position.x)
            {
				TargetPlayer();

				transform.position = Vector2.MoveTowards(transform.position, spots[2].position, speed * Time.deltaTime);
                //Only changes Y and X Position

                yield return null;
            }

			yield return new WaitForSeconds(.5f);

			Burst();

			yield return new WaitForSeconds(1f);
            
            while (transform.position.x != spots[1].position.x)
            {
				TargetPlayer();

				transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed * Time.deltaTime);
                yield return null;
            }

			yield return new WaitForSeconds(.5f);

			Burst();

            yield return new WaitForSeconds(1f);
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
		speed = 3.0f;
		float bomberDelay = 5.0f;

		while (true)
		{
			bomberDelay -= Time.deltaTime;

			if (bomberDelay < 0.0f)
			{
				SummonBomber();
				bomberDelay = 5.0f;
			}

			transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
			yield return null;
		}
	}
}
  /*  void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player" && vulnerable)
        {
            hp -= 30;
            vulnerable = false;
            //GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }

}*/