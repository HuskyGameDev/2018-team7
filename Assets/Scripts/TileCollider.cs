using UnityEngine;

public class TileCollider : MonoBehaviour
{
	private BoxCollider col;
	public Tile tile { get; set; }

	private void Awake()
	{
		col = GetComponent<BoxCollider>();
	}

	/// <summary>
	/// Set the given tile for this collider. During collision handling, the
	/// tile can be accessed to see which tile was collided with.
	/// </summary>
	public void SetTile(Tile tile)
	{
		this.tile = tile;
	}

	/// <summary>
	/// Set the world space position of this collider.
	/// </summary>
	public void SetPosition(Vector2 pos)
	{
		transform.position = pos;
	}

	/// <summary>
	/// Set the size of this collider.
	/// </summary>
	public void SetSize(Vector2 size)
	{
		col.size = new Vector3(size.x, size.y, 5.0f);
	}

	/// <summary>
	/// Set the trigger state of this collider.
	/// Trigger colliders will not block movement but will detect overlap.
	/// </summary>
	public void SetTrigger(bool trigger)
	{
		col.isTrigger = trigger;
	}

	public void Enable()
	{
		col.enabled = true;
	}

	public void Disable()
	{
		col.enabled = false;
	}
}
