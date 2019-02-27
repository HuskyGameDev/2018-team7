using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
	private PlayerController playerController;
	private Floor floor;
	private Stats stats;
	private RectTransform t;

	public Text text;

	// Use this for initialization
	void Start()
	{
		t = GetComponent<RectTransform>();

		GameObject playerControllerObject = GameObject.FindWithTag("Player");
		GameObject floorObject = GameObject.FindWithTag("Floor");
		GameObject statsObject = GameObject.FindWithTag("Stats");

		stats = statsObject.GetComponent<Stats>(); //Find the stats component on the Stats gameObject

		if (playerControllerObject != null)
		{
			playerController = playerControllerObject.GetComponent<PlayerController>();
			floor = floorObject.GetComponent<Floor>();


		}
		else
		{
			Debug.Log("Cannot find Player");
		}


	}

	// Update is called once per frame
	void Update()
	{
		text.text = floor.FloorID.ToString(); //Update the UI for the player
		stats.setScore(floor.FloorID); //Update the stats file when the floor changes
        PlayerPrefs.SetInt("Score", floor.FloorID);
		float pos = ((playerController.health / 100f) * 130f) - 65f;
		t.anchoredPosition = new Vector2(pos, t.anchoredPosition.y);
	}
}
