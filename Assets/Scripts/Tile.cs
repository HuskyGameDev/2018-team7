using System;

// Using a structure for tiles in case we want to support more advanced features later.
public struct Tile : IEquatable<Tile>
{
	public TileType id;

	public Tile(TileType id)
	{
		this.id = id;
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
