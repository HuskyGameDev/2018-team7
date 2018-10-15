using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TestPlayer : MonoBehaviour
{
	[SerializeField] private float speed;

	private CharacterController controller;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();	
	}

	private void Update()
	{
		Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		controller.Move(move * speed * Time.deltaTime);
	}
}
