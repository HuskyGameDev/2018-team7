using System;

// Using a structure for tiles in case we want to support more advanced features later.
public struct Tile : IEquatable<Tile>
{
	/// <summary>
	/// The ID of the tile.
	/// </summary>
	public TileType id;

	/// <summary>
	/// The variant index specifies which variant this tile is set to.
	/// Variants allow tiles to have different forms and properties while
	/// using the same ID.
	/// </summary>
	public ushort variant;

	public Tile(TileType id, ushort variant = 0)
	{
		this.id = id;
		this.variant = variant;
	}

	/// <summary>
	/// Return the properties for this tile.
	/// </summary>
	public TileProperties Properties
	{
		get { return Floor.Instance.GetTileProperties(this); }
	}

	public static implicit operator Tile(TileType id)
	{
		return new Tile(id);
	}

	public static bool operator ==(Tile a, TileType b)
	{
		return a.id == b;
	}

	public static bool operator ==(Tile a, Tile b)
	{
		return a.id == b.id;
	}

	public static bool operator !=(Tile a, Tile b)
	{
		return a.id != b.id;
	}

	public static bool operator !=(Tile a, TileType b)
	{
		return a.id != b;
	}

	public bool Equals(Tile other)
	{
		return id == other.id;
	}

	public override bool Equals(object obj)
	{
		return Equals((Tile)obj);
	}

	public override int GetHashCode()
	{
		return id.GetHashCode();
	}
}
