using System;

[Serializable]
public sealed class TileData : IEquatable<TileData>
{
	public string name;
	public TileType type;
	public int variantCount;
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
