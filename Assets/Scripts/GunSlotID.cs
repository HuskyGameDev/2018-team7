using UnityEngine;
using System.Collections;
using System;

// Note: this is a temporary fix for the gun slot ordering issue I encountered. 
// It's not the best design but works for now. 
public class GunSlotID : MonoBehaviour, IComparable<GunSlotID>
{
	[Header("Gun ID value from GunType at Gun.cs")]
	public int value;

	public int CompareTo(GunSlotID other)
	{
		return value.CompareTo(other.value);
	}
}
