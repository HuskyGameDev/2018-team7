using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static Utils;

using Debug = UnityEngine.Debug;

public class EnemyController : MonoBehaviour
{
	public float detectRange;
	[HideInInspector] public float speed;

	private CharacterController controller;
	private SpriteRenderer rend;
	private PlayerController pc;

	private int health = 10;
	public Room room;

	// The path the enemy will follow.
	private Stack<Vector2> path = new Stack<Vector2>();

	private bool followingPath;
	//private bool pathDrawn;
	private Vector2? nextCell;

	private float timer;

	public Vector2 Pos
	{
		get { return transform.position; }
	}

	// Use this for initialization
	void Start()
    {
        //StartCoroutine(WaitTime(2));
        speed = 2.5f;

		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();

		pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

		transform.SetZ(-0.1f);

		timer = Random.Range(1.5f, 2.5f);
	}

	private void PathFinished()
	{
		nextCell = path.Pop();
		//pathDrawn = false;
	}

	private void GetPath()
	{
		Floor floor = Floor.Instance;
		floor.Pathfinder.FindPath(TilePos(transform.position), TilePos(pc.FeetPosition), path, PathFinished);
		followingPath = true;
	}

	private void Move(Vector2 dir)
	{
		controller.Move(dir * speed * Time.deltaTime);
		transform.SetZ(-.1f);
	}

	// Update is called once per frame
	// Changes the direction of the enemy's movement towards the player once per frame
	void Update()
    {
		if (pc.Dead || Time.timeScale == 0.0f)
			return;

		if (path.Count == 0)
			followingPath = false;

		timer -= Time.deltaTime;

		//DrawPath();

		if (nextCell.HasValue)
		{
			if (timer <= 0.0f)
			{
				GetPath();
				timer = 1.0f;
			}

			Vector2 next = nextCell.Value;
			Vector2 dir = (next - Pos).normalized;

			Move(dir);

			if ((next - Pos).sqrMagnitude < 0.09f)
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
			float dist = Vector2.Distance(Pos, pc.FeetPosition);

			if (dist <= 1.0f)
			{
				Vector2 dir = ((Vector2)pc.FeetPosition - Pos).normalized;
				Move(dir);
			}
			else
			{
				if (!followingPath && dist <= detectRange)
					GetPath();
			}
		}
	}

	//private void DrawPath()
	//{
	//	if (!pathDrawn && followingPath && path.Count > 0)
	//	{
	//		Vector3[] v3 = new Vector3[path.Count];

	//		int i = 0;
	//		foreach (Vector2 v in path)
	//			v3[i++] = new Vector3(v.x, v.y, -5.0f);

	//		GameObject line = new GameObject("Debug Path");
	//		LineRenderer lR = line.AddComponent<LineRenderer>();
	//		lR.widthMultiplier = 0.2f;
	//		lR.positionCount = v3.Length;
	//		lR.SetPositions(v3);

	//		Destroy(line, 10.0f);
	//		pathDrawn = true;
	//	}
	//}

	IEnumerator WaitTime(int time)
    {
        yield return new WaitForSeconds(time);
    }

	public void ApplyDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			SpawnWeapon(transform.position);
			Destroy(gameObject);
		}
			
		else StartCoroutine(TintRed());
	}

	private IEnumerator TintRed()
	{
		rend.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		rend.color = Color.white;
	}

	// Spawns a new weapon with at a 25% chance at the Vector3 position
	public void SpawnWeapon(Vector3 pos)
	{
		GetComponent<DropRate>().SpawnGun(pos.x, pos.y);
	}
}