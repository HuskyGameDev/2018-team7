using UnityEngine;

// This is a temporary fix since the required BulletController needed by other scripts was not included.
// We need the project to compile so we can work on our parts of the project.
// Delete this when the actual bullet controller is put in.
public class BulletController : MonoBehaviour
{
	public float speedX = 0.0f;
	public float speedY = 0.0f;
}
