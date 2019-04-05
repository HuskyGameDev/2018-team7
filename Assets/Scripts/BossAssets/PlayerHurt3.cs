using UnityEngine;
using System.Collections;

public class PlayerHurt3 : MonoBehaviour {

    public int damageToGive;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //other.gameObject.GetComponent<PlayerHealthLevel4>().HurtEnemy(damageToGive);
        }

    }
}
