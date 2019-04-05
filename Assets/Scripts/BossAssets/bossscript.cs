using UnityEngine;
using System.Collections;

public class bossscript : MonoBehaviour
{
    public Transform[] spots;
    public float speed;
    public GameObject projectile;
    GameObject Player;
    public Transform[] holes;
    Vector3 playerPos;
    public bool vulnerable;

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

    IEnumerator boss()
    {
        while (true)
        {
            //FIRST ATTACK

            while (transform.position.x != spots[0].position.x)
            {
                //Only changes X Position
                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, transform.position.y), speed);
                transform.position = Vector2.MoveTowards(transform.position, spots[0].position, speed);

                yield return null;
            }

            //transform.localScale = new   Vector2(-1, 1);

            //Delay before starting to shoot
            yield return new WaitForSeconds(.5f);

            int i = 0;
            while (i < 1)
            {

                GameObject bullet = (GameObject)Instantiate(projectile, holes[Random.Range(0, 2)].position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = Vector2.left * 10;

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
                transform.position = Vector2.MoveTowards(transform.position, spots[2].position, speed);
                //Only changes Y and X Position

                yield return null;
            }

            //transform.localScale = new Vector2(-1, 1);

            yield return new WaitForSeconds(2f);

            i = 0;
            while (i < 1)
            {

                GameObject bullet = (GameObject)Instantiate(projectile, holes[Random.Range(0, 2)].position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = Vector2.down * 8;

                i++;
                yield return new WaitForSeconds(3f);
                // should be .7f
            }

            //playerPos = Player.transform.position;
            //delay before moving
            yield return new WaitForSeconds(2f);
            

            while (transform.position.x != spots[1].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed);
               

                yield return null;
            }

            yield return new WaitForSeconds(2f);

            i = 0;
            while (i < 1)
            {

                GameObject bullet = (GameObject)Instantiate(projectile, holes[Random.Range(0, 2)].position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = Vector2.right * 10;

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
   
            while (transform.position.x != spots[0].position.x)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, transform.position.y), speed);

                yield return null;
            }

         

            yield return new WaitForSeconds(2f);

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