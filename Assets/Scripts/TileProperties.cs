using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public sealed class TileProperties
{
	// Filled in by the tile data editor.

	public bool invisible, hasCollider, trigger;
	public Vector2 colliderSize, colliderOffset;
	public Vector3 renderOffset;
	public Sprite sprite;
	public Color32 color;
}