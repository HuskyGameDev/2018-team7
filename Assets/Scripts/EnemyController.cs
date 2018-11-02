using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Transform target;

	private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(WaitTime(2));
        speed = 5f;

		controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    // Changes the direction of the enemy's movement towards the player once per frame
    void Update()
    {
        //transform.LookAt(target.position);
        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        // if away from the player move towards him/her
        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            Vector3 pcDirection = (target.position - transform.position).normalized;
            controller.Move(pcDirection * speed * Time.deltaTime);
            transform.SetZ(-.1f);
        }

        // if on top of the player, slow down
        if (Vector3.Distance(transform.position, target.position) == 0f)
        {
            speed -= 2f;
            StartCoroutine(WaitTime(1));
            speed += 2f;
        }
    }

    IEnumerator WaitTime(int timer)
    {
        yield return new WaitForSeconds(timer);
    }
}