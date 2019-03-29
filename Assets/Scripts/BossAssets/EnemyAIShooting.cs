using UnityEngine;
using System.Collections;

public class EnemyAIShooting : MonoBehaviour {

   public  float rotSpeed = 90f;
    Transform Player;
	
	// Update is called once per frame
	void Update () {
	if(Player == null)
        {
            GameObject go = GameObject.Find("Player");

            if(go != null)
            {
                Player = go.transform;
            }
        }
        if (Player == null)
            return;

        Vector3 dir = Player.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desireRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desireRot, rotSpeed * Time.deltaTime);
	}
}
