using UnityEngine;
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

	// Update is called once per frame
	// Update the UI to display the image for each of the objects
	void Update()
	{
		for (int i = 0; i < GunType.Count; i++)
		{
			if (!loaded[i] && playerController.HasGun(i))
			{
				Transform t = Instantiate(gunSlots[i], verticalLayout).transform;
				t.SetSiblingIndex(i);
				loaded[i] = true;
			}
		}
	}
}
