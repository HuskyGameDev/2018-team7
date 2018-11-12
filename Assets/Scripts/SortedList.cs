//
// Copyright (c) 2018 Jason Bricco
//

using System.Collections.Generic;

public sealed class SortedList<T, U>
{
	private Dictionary<T, U> table;
	private List<U> values;

	public SortedList()
	{
		table = new Dictionary<T, U>();
		values = new List<U>();
	}

	public int Count => values.Count;

	public void Add(T key, U item)
	{
		U value;
		if (table.TryGetValue(key, out value))
			return;

		table.Add(key, item);
		Add(item);
	}

	public void Add(U item)
	{
		int index = values.BinarySearch(item);
		if (index < 0) index = ~index;

		values.Insert(index, item);
	}

	public bool TryGetValue(T key, out U value)
		=> table.TryGetValue(key, out value);

	public void Remove(U item)
	{
		values.Remove(item);
	}

	public void RemoveFirst(T key, U item)
	{
		table.Remove(key);
		values.RemoveAt(0);
	}

	public U First => values[0];

	public void Clear()
	{
		table.Clear();
		values.Clear();
	}

	public U this[int i] => values[i];
}