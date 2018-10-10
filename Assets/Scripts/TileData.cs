using System;

[Serializable]
public sealed class TileData : IEquatable<TileData>
{
	/// <summary>
	/// The human-readable name of the tile.
	/// </summary>
	public string name;

	/// <summary>
	/// The tile's type.
	/// </summary>
	public TileType type;

	/// <summary>
	/// How many variants this tile supports.
	/// </summary>
	public int variantCount;

	/// <summary>
	/// A list of property objects for each variant.
	/// </summary>
	public TileProperties[] variants;

	public TileData()
	{
		variantCount = 1;
		variants = new TileProperties[variantCount];

		variants[0] = new TileProperties();
	}

	public TileProperties GetProperties(int variant) => variants[variant];

	public void InitializeProperties()
	{
		for (int i = 0; i < variants.Length; i++)
		{
			if (variants[i] == null)
				variants[i] = new TileProperties();
		}
	}

	public bool Equals(TileData other) => type == other.type;
	public override int GetHashCode() => type.GetHashCode();
}
