using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int MaxHealth;
    public int CurHealth;

    // Use this for initialization
    void Start () {
        CurHealth = MaxHealth;
    }
	
	// Update is called once per frame
	void Update () {
	if(CurHealth <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void HurtEnemy(int damageToGive)
    {
        CurHealth -= damageToGive;
    }

  //  public void SetMaxHealth()
   // {
  //      CurHealth = MaxHealth;
   // }

}
