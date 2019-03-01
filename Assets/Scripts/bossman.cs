using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossman : MonoBehaviour
{
    //FIRST PHASE
    float health = 100;
    public Transform[] bossPos;
    public Transform[] holes;
    public GameObject rocket;
    GameObject player;
    public float speed;
    Vector3 playerPos;
    //SECOND PHASE
    //public Transform player;
    public Transform myTransform;
    public float maxSpeed;
    //THIRD PHASE

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        StartCoroutine("Boss");

        //transform.position = waypoints[waypointIndex].transform.position;


    }

    // Update is called once per frame
    void Update()
    {
       // while (true)
        {
            //PHASE THREE
           // if (health <= 10)
            {
              

            }
            //PHASE TWO
           // else if (health <= 50 && health > 10)
            {
               // transform.LookAt(player);
                //transform.Translate(Vector3.forward * maxSpeed * Time.deltaTime);
            }
        }

    }
    //PHASE ONE
    IEnumerator Boss()
    {

       //while (health <= 100 && health > 50) 
       // {
            //FIRST ATTACK
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            while (transform.position.x != bossPos[0].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(bossPos[0].position.x, transform.position.y), speed);
                yield return null;

            }
            transform.localScale = new Vector2(-1, 1);
            yield return new WaitForSeconds(.1f);
            int b = 0;
            while (b < 2)
            {
                GameObject rock = (GameObject)Instantiate(rocket, holes[Random.Range(0, 2)].position, Quaternion.identity);
                rock.GetComponent<Rigidbody2D>().velocity = Vector2.left * 5;
                b++;
                yield return new WaitForSeconds(.7f);
            }
            //SECOND ATTACK
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //GetComponent<Rigidbody2D>().isKinematic = true;
            yield return new WaitForSeconds(.1f);

            while (transform.position.x != bossPos[2].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, bossPos[2].position, speed);
                yield return null;

            }

            int x = 0;
            while (x < 2)
            {
                GameObject rock = (GameObject)Instantiate(rocket, holes[Random.Range(0, 2)].position, Quaternion.identity);
                rock.GetComponent<Rigidbody2D>().velocity = Vector2.right * 5;
                x++;
                yield return new WaitForSeconds(.7f);
            }
            //THIRD ATTACK
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //GetComponent<Rigidbody2D>().isKinematic = false;
            yield return new WaitForSeconds(.1f);
            while (transform.position != bossPos[1].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, bossPos[1].position, speed);
                yield return null;
            }

            yield return new WaitForSeconds(.7f);

        }
    }
//}

