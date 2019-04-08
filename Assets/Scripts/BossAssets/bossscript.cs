using UnityEngine;
using System.Collections;

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

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("boss");
		enemy = GetComponent<Enemy>();
	}

    // Update is called once per frame
    void Update()
    {
        if (enemy.health <= 0 && !dead)
        {
            dead = true;
            GetComponent<SpriteRenderer>().color = Color.gray;
			StopCoroutine("boss");
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

    IEnumerator boss()
    {
		while (true)
        {
            //FIRST ATTACK

            while (transform.position.x != spots[0].position.x)
            {
                //Only changes X Position
                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, transform.position.y), speed);
                transform.position = Vector2.MoveTowards(transform.position, spots[0].position, speed * Time.deltaTime);

                yield return null;
            }

            //transform.localScale = new   Vector2(-1, 1);

            //Delay before starting to shoot
            yield return new WaitForSeconds(.5f);

            int i = 0;
            while (i < 1)
            {

				Bullet bullet = bullets.CreateBullet(transform, hole);
				bullet.ChangeFacing(Facing.Right);
				bullet.SetSpeed(50.0f);
				bullet.gameObject.layer = 15;

				i++;
                //Delay between shots
                yield return new WaitForSeconds(3f);
                // should be .7f
            }

            yield return new WaitForSeconds(2f);
            //SECOND ATTACK
            //    GetComponent<Rigidbody2D>().isKinematic = true;
            while (transform.position.x != spots[2].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[2].position, speed * Time.deltaTime);
                //Only changes Y and X Position

                yield return null;
            }

            //transform.localScale = new Vector2(-1, 1);

            yield return new WaitForSeconds(2f);

            i = 0;
            while (i < 1)
            {

				Bullet bullet = bullets.CreateBullet(transform, hole);
				bullet.ChangeFacing(Facing.Left);
				bullet.SetSpeed(50.0f);
				bullet.gameObject.layer = 15;

				i++;
                yield return new WaitForSeconds(3f);
                // should be .7f
            }

            //playerPos = Player.transform.position;
            //delay before moving
            yield return new WaitForSeconds(2f);
            

            while (transform.position.x != spots[1].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed * Time.deltaTime);
               

                yield return null;
            }

            yield return new WaitForSeconds(2f);

            i = 0;
            while (i < 1)
            {

				Bullet bullet = bullets.CreateBullet(transform, hole);
				bullet.ChangeFacing(Facing.Front);
				bullet.SetSpeed(50.0f);
				bullet.gameObject.layer = 15;

				i++;
                yield return new WaitForSeconds(3f);
            }

            //  this.tag = "Untagged";
            // GetComponent<SpriteRenderer>().sprite = sprites[1];
            //   vulnerable = true;
            yield return new WaitForSeconds(2f);
            //   this.tag = "deadly";
            // GetComponent<SpriteRenderer>().sprite = sprites[0];
            //  vulnerable = false;

            //THIRD ATTACK


            /*  Transform temp;
              if (transform.position.x > Player.transform.position.x)
                  temp = spots[1];
              else
                  temp = spots[0];

              while (transform.position.x != temp.position.x)
              {

                  transform.position = Vector2.MoveTowards(transform.position, new Vector2(temp.position.x, transform.position.y), speed);

                  yield return null;
              }
              */
  

           /* i = 0;
            while (i < 6)
            {

                GameObject bullet = (GameObject)Instantiate(projectile, holes[Random.Range(0, 2)].position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * 20;

                i++;
                yield return new WaitForSeconds(.5f);
                // should be .7f
            }*/

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