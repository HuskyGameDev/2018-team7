using UnityEngine;
using System;

[Serializable]
public sealed class TileProperties
{
	/// <summary>
	/// If true, the tile will not receive a sprite and will be invisible.
	/// </summary>
	public bool invisible;

	/// <summary>
	/// If false, this tile will not participate in collision detection.
	/// </summary>
	public bool hasCollider;

	/// <summary>
	/// If true, the tile will use a trigger collider.
	/// </summary>
	public bool trigger;

	/// <summary>
	/// The size of the tile's collider. By default it is the size of the tile's sprite.
	/// </summary>
	public Vector2 colliderSize;

	/// <summary>
	/// Used to apply an offset for the collider in case it shouldn't be placed exactly
	/// where the sprite is.
	/// </summary>
	public Vector2 colliderOffset;

	/// <summary>
	/// Apply an offset to the sprite in case the position it is placed by default is undesirable.
	/// </summary>
	public Vector3 renderOffset;

	/// <summary>
	/// The tile's sprite.
	/// </summary>
	public Sprite sprite;

	/// <summary>
	/// Used to apply a color tint to the tile.
	/// </summary>
	public Color32 color;

	/// <summary>
	/// Currently unused - can be used for special tile functionality.
	/// </summary>
	public TileComponent component;
}
