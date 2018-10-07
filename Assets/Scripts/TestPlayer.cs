using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestPlayer : MonoBehaviour
{
	[SerializeField] private float speed;

	private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();		
	}

	private void Update()
	{
		Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		rb.AddForce(move * Time.deltaTime * speed, ForceMode2D.Impulse);
	}
}
