using UnityEngine;

public class Gun : MonoBehaviour
{
	protected GameObject bullet;
	protected PlayerController pc;
	public float speed; // the speed at which the player shoots

	private void Start()
	{
		pc = GetComponent<PlayerController>();
		bullet = Resources.Load<GameObject>("Bullet");
		Init();
	}

	protected virtual void Init()
	{
		speed = 0.2f;
	}
}
