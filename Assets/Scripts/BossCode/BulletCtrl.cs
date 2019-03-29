using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {

    public Vector2 speed;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;

      
	}
   
    // Update is called once per frame
    void Update () {
        rb.velocity = speed;
	}

}
