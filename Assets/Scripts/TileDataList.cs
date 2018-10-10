using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Stores tile data for the game, filled out by the tile data editor. 
/// </summary>
public class TileDataList : ScriptableObject
{
	[SerializeField] private TileData[] data;

	/// <summary>
	/// Create a new tile data list. It will use the tile types defined in the TileType
	/// enumeration to add a data object for each.
	/// </summary>
	public void Init()
	{
		TileType[] values = (TileType[])Enum.GetValues(typeof(TileType));
		string[] names = Enum.GetNames(typeof(TileType));

		data = new TileData[names.Length];

		for (int i = 0; i < Count; i++)
		{
			data[i] = new TileData();
			data[i].name = names[i];
			data[i].type = values[i];
		}
	}

	/// <summary>
	/// The number of tiles in this data list.
	/// </summary>
	public int Count
	{
		get
		{
			if (data == null) return -1;
			return data.Length;
		}
	}

	public TileProperties GetProperties(Tile tile)
	{
		return data[(int)tile.id].GetProperties(tile.variant);
	}

	/// <summary>
	/// Scan the tile types defined and ensure this tile data list matches them.
	/// </summary>
	public bool Refresh()
	{
		try
		{
			Dictionary<string, TileData> map = new Dictionary<string, TileData>(data.Length);

			for (int i = 0; i < data.Length; i++)
				map.Add(data[i].name, data[i]);

			string[] names = Enum.GetNames(typeof(TileType));
			TileType[] values = (TileType[])Enum.GetValues(typeof(TileType));

			List<TileData> newData = new List<TileData>(names.Length);

			for (int i = 0; i < names.Length; i++)
			{
				TileData td;
				if (map.TryGetValue(names[i], out td))
					newData.Add(td);
				else
				{
					td = new TileData();
					td.name = names[i];
					td.type = values[i];
					newData.Add(td);
				}
			}

			data = newData.ToArray();
			return true;
		}
		catch (Exception e)
		{
			Debug.LogWarning("Exception occurred: " + e.Message);
			Debug.LogWarning(e.StackTrace);
			return false;
		}
	}

	public TileData this[int i]
	{
		get { return data[i]; }
	}
}