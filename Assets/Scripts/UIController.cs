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
	private RectTransform[] transforms;

	private bool[] loaded = new bool[GunType.Count];

	private PlayerController pc;

	private void Awake()
	{
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		transforms = new RectTransform[gunSlots.Length];
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
			// Until we get pistol art I want to leave the pistol slot at size 32, 32 (pistol is i == 0).
			if (i != 0 && loaded[i])
				transforms[i].sizeDelta = i == pc.Gun ? new Vector2(72.0f, 72.0f) : new Vector2(45.0f, 45.0f);

			if (!loaded[i] && pc.HasGun(i))
			{
				RectTransform t = Instantiate(gunSlots[i], verticalLayout).GetComponent<RectTransform>();
				transforms[i] = t;
				Reorder();
				loaded[i] = true;
			}
		}
	}
}
