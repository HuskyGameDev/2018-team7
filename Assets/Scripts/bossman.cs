using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossman : MonoBehaviour {

    public Transform[] bossPos;
    public Transform[] holes;
    public GameObject rocket;
    GameObject player;
    public float speed;
    Vector3 playerPos;

    public Sprite[] sprites;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("player");
        StartCoroutine("Boss");	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Boss()
    {
        //working
        //moves right and shoots left
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        while (transform.position.x != bossPos[0].position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(bossPos[0].position.x, transform.position.y), speed);
            yield return null;

        }
        transform.localScale = new Vector2(-1, 1);
        yield return new WaitForSeconds(.1f);
        int b = 0;
        while (b < 4)
        {
            GameObject rock = (GameObject)Instantiate(rocket, holes[Random.Range(0, 2)].position, Quaternion.identity);
            rock.GetComponent<Rigidbody2D>().velocity = Vector2.left * 5;
            b++;
            yield return new WaitForSeconds(.7f);
        }
        //moves left and shoots right
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        GetComponent<Rigidbody2D>().isKinematic = true;
        while (transform.position.x != bossPos[2].position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(bossPos[2].position.x, transform.position.y), speed);
            yield return null;

        }
        int x = 0;
        while (x < 4)
        {
            GameObject rock = (GameObject)Instantiate(rocket, holes[Random.Range(0, 2)].position, Quaternion.identity);
            rock.GetComponent<Rigidbody2D>().velocity = Vector2.right * 5;
            x++;
            yield return new WaitForSeconds(.7f);
        }
        yield return new WaitForSeconds(.1f);
        //transform.localScale = new Vector2(-1, 1);
        //moves up and shoots down
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //playerPos = player.transform.position;
        yield return new WaitForSeconds(.1f);
        while (transform.position != bossPos[1].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossPos[1].position, speed);
            yield return null;
        }
        int y = 0;
        while (y < 4)
        {
            GameObject rock = (GameObject)Instantiate(rocket, holes[Random.Range(0, 2)].position, Quaternion.identity);
            rock.GetComponent<Rigidbody2D>().velocity = Vector2.down * 5;
            y++;
            yield return new WaitForSeconds(.7f);
        }
        yield return new WaitForSeconds(.1f);
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
