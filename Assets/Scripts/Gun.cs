using UnityEngine;

public class Gun : MonoBehaviour
{
	protected GameObject bullet;
	protected PlayerController pc;
	public float speed; // the speed at which the player shoots
	public AudioSource audioSource;

	protected void Start()
	{
		pc = GetComponent<PlayerController>();
		audioSource = GetComponent<AudioSource>();
		bullet = Resources.Load<GameObject>("Bullet");
		Init();
	}

	protected virtual void Init()
	{
		speed = 0.2f;
	}
}
