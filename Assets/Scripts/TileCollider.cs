using UnityEngine;

public class TileCollider : MonoBehaviour
{
	private BoxCollider2D col;
	public Tile tile { get; set; }

	private void Awake()
	{
		col = GetComponent<BoxCollider2D>();
	}

	public void SetTile(Tile tile)
	{
		this.tile = tile;
	}

	public void SetPosition(Vector2 pos)
	{
		transform.position = pos;
	}

	public void SetSize(Vector2 size)
	{
		col.size = size;
	}

	public void SetTrigger(bool trigger)
	{
		col.isTrigger = trigger;
	}
}
