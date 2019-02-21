using UnityEngine;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Transform verticalLayout;

	[Header("Note: these should match the order defined in the ", order=0)]
	[Space(-10, order=1)]
	[Header("GunType enum in Gun.cs", order=3)]
	[SerializeField] private GameObject[] gunSlots;

	private bool[] loaded = new bool[GunType.Count];

	private PlayerController playerController;

	private void Awake()
	{
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}

	// Inserts the gun slot into the correct location based on its ID value.
	// This ensures the gun slots are shown in the correct order (1 to 5).
	private void Reorder()
	{
		GunSlotID[] slots = verticalLayout.GetComponentsInChildren<GunSlotID>();
		Array.Sort(slots);

		for (int i = 0; i < slots.Length; i++)
			slots[i].transform.SetParent(null);

		for (int i = 0; i < slots.Length; i++)
			slots[i].transform.SetParent(verticalLayout);
	}

	// Update is called once per frame
	// Update the UI to display the image for each of the objects
	void Update()
	{
		for (int i = 0; i < GunType.Count; i++)
		{
			if (!loaded[i] && playerController.HasGun(i))
			{
				Instantiate(gunSlots[i], verticalLayout);
				Reorder();
				loaded[i] = true;
			}
		}
	}
}
