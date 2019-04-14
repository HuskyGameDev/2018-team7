using UnityEngine;
using System.Collections.Generic;
using static Utils;

/// <summary>
/// Put this on a GameObject to give it follow AI. It will follow the player if the player
/// is within the detect range.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class EnemyFollowAI : MonoBehaviour
{
	public float baseSpeed;
	public float detectRange;

	private float speed;

	private CharacterController controller;

	// The path the enemy will follow.
	private Stack<Vector2> path = new Stack<Vector2>();

	private bool followingPath;
	private Vector2? nextCell;

	private float timer;

	private Enemy enemy;

	private float startDelay = 0.5f;

	void Awake()
	{
		int floor = Floor.Instance.FloorID;

		// Set the enemy's speed based on the floor we're on. For each additional floor,
		// the enemy will get 0.25 higher speed up to a max speed of 10.0.
		speed = Mathf.Min(baseSpeed + ((floor - 1) * 0.25f), 10.0f);
		Debug.Log("Ran awake");

		controller = GetComponent<CharacterController>();
		transform.SetZ(-0.1f);

		timer = Random.Range(1.5f, 2.5f);
		enemy = GetComponent<Enemy>();
	}

	private void PathFinished()
	{
		nextCell = path.Pop();
	}

	public void SetSpeed(float speed)
	{
		this.speed = speed;
		Debug.Log("Set speed");
	}

	private void GetPath()
	{
		Floor floor = Floor.Instance;
		floor.Pathfinder.FindPath(TilePos(transform.position), TilePos(enemy.pc.FeetPosition), path, PathFinished);
		followingPath = true;
	}

	private void Move(Vector2 dir)
	{
		controller.Move(dir * speed * Time.deltaTime);
		transform.SetZ(-.1f);
	}

	void Update()
	{
		startDelay -= Time.deltaTime;

		if (startDelay < 0.0f)
		{
			timer -= Time.deltaTime;

			if (enemy.pc.Dead || Time.timeScale == 0.0f)
				return;

			if (path.Count == 0)
				followingPath = false;

			if (nextCell.HasValue)
			{
				if (timer <= 0.0f)
				{
					GetPath();
					timer = 0.5f;
				}

				Vector2 next = nextCell.Value;
				Vector2 dir = (next - enemy.Pos).normalized;

				Move(dir);

				if ((next - enemy.Pos).sqrMagnitude < 0.09f)
				{
					if (path.Count > 0)
						nextCell = path.Pop();
					else
					{
						nextCell = null;
						followingPath = false;
					}
				}
			}
			else
			{
				float dist = Vector2.Distance(enemy.Pos, enemy.pc.FeetPosition);

				if (dist <= 1.0f)
				{
					Vector2 dir = (enemy.pc.FeetPosition - enemy.Pos).normalized;
					Move(dir);
				}
				else
				{
					if (!followingPath && dist <= detectRange)
						GetPath();
				}
			}
		}
	}
}
