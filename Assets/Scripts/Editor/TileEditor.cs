using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions;
using System;
using static UnityEngine.Mathf;

public class TileEditor : EditorWindow
{
	private TileDataList data;
	private Vector2 scroll;

	private bool[] collapsed = new bool[0];

	[MenuItem("Window/Tile Editor")]
	public static void ShowWindow()
	{
		GetWindow<TileEditor>("Tile Editor");
	}

	private void LoadData()
	{
		data = AssetDatabase.LoadAssetAtPath<TileDataList>("Assets/Data/TileData.asset");

		if (data == null)
		{
			Debug.Log("TileDataList not found, creating a new one.");
			data = CreateInstance<TileDataList>();
			data.Init();
		}
		else Debug.Log("Successfully loaded the TileDataList.");
	}

	private void SaveAsset()
	{
		if (!AssetDatabase.Contains(data))
			AssetDatabase.CreateAsset(data, "Assets/Data/TileData.asset");

		EditorUtility.SetDirty(data);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	private void ResetData()
	{
		AssetDatabase.DeleteAsset("Assets/Data/TileData.asset");
		data = null;
	}

	private void OnGUI()
	{
		if (data == null)
		{
			LoadData();
			Assert.IsNotNull(data);
		}

		if (data.Count == -1)
		{
			Debug.LogWarning("TileData asset exists, but contains no data!");
			ResetData();
			return;
		}

		if (collapsed.Length != data.Count)
		{
			collapsed = new bool[data.Count];

			for (int i = 0; i < collapsed.Length; i++)
				collapsed[i] = true;
		}

		if (GUI.Button(new Rect(position.width - 140.0f, 15.0f, 100.0f, 30.0f), "Save"))
			SaveAsset();

		if (GUI.Button(new Rect(position.width - 140.0f, 50.0f, 100.0f, 30.0f), "Refresh"))
		{
			if (!data.Refresh())
			{
				Debug.LogWarning("Something went wrong. Resetting the tile data.");
				ResetData();
				return;
			}
		}

		float y = 15.0f;

		scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Width(position.width), GUILayout.Height(position.height));

		for (int i = 0; i < data.Count; i++)
		{
			TileData td = data[i];

			if (collapsed[i])
			{
				GUI.contentColor = Color.gray;
				EditorGUI.LabelField(new Rect(15.0f, y, 100.0f, 20.0f), td.name, EditorStyles.boldLabel);
				GUI.contentColor = Color.white;

				GUI.color = GUI.color.SetAlpha(0.0f);

				if (GUI.Button(new Rect(15.0f, y, 100.0f, 20.0f), GUIContent.none))
					collapsed[i] = false;

				GUI.color = GUI.color.SetAlpha(1.0f);
				y += 20.0f;
				continue;
			}
			else
			{
				GUI.contentColor = Color.green;
				EditorGUI.LabelField(new Rect(15.0f, y, 100.0f, 20.0f), td.name, EditorStyles.boldLabel);
				GUI.contentColor = Color.white;

				GUI.color = GUI.color.SetAlpha(0.0f);

				if (GUI.Button(new Rect(15.0f, y, 100.0f, 20.0f), GUIContent.none))
					collapsed[i] = true;

				GUI.color = GUI.color.SetAlpha(1.0f);
			}

			y += 25.0f;
			td.type = (TileType)EditorGUI.EnumPopup(new Rect(15.0f, y, 120.0f, 20.0f), td.type);

			y += 25.0f;
			EditorGUI.BeginChangeCheck();

			EditorGUI.LabelField(new Rect(15.0f, y, 50.0f, 20.0f), "Variants");
			td.variantCount = EditorGUI.IntField(new Rect(80.0f, y, 25.0f, 20.0f), td.variantCount);

			if (EditorGUI.EndChangeCheck())
			{
				td.variantCount = Clamp(td.variantCount, 1, 100);
				Array.Resize(ref td.variants, td.variantCount);
				td.InitializeProperties();
			}

			y += 25.0f;

			float x = 15.0f;
			float lastLeftY = 0.0f, lastRightY = 0.0f;

			for (int v = 0; v < td.variantCount; v++)
			{
				float startY = y;

				if (td.variantCount != 1)
				{
					GUI.contentColor = Color.magenta;
					EditorGUI.LabelField(new Rect(x, y, 80.0f, 20.0f), "- " + v.ToString() + " -", EditorStyles.boldLabel);
					GUI.contentColor = Color.white;
					y += 25.0f;
				}

				TileProperties props = td.GetProperties(v);

				EditorGUI.LabelField(new Rect(x, y, 60.0f, 20.0f), "Invisible");
				props.invisible = EditorGUI.Toggle(new Rect(x + 55.0f, y, 15.0f, 25.0f), props.invisible);

				EditorGUI.LabelField(new Rect(x + 85.0f, y, 80.0f, 20.0f), "Has Collider");
				props.hasCollider = EditorGUI.Toggle(new Rect(x + 160.0f, y, 15.0f, 25.0f), props.hasCollider);

				if (props.hasCollider)
				{
					EditorGUI.LabelField(new Rect(x + 190.0f, y, 60.0f, 20.0f), "Trigger");
					props.trigger = EditorGUI.Toggle(new Rect(x + 240.0f, y, 15.0f, 25.0f), props.trigger);

					y += 25.0f;
					props.colliderSize = EditorGUI.Vector2Field(new Rect(x, y, 120.0f, 20.0f), "Collider Size", props.colliderSize);
					props.colliderOffset = EditorGUI.Vector2Field(new Rect(x + 135.0f, y, 120.0f, 20.0f), "Collider Offset", props.colliderOffset);
					y += 15.0f;
				}

				if (!props.invisible)
				{
					y += 25.0f;
					props.renderOffset = EditorGUI.Vector3Field(new Rect(x, y, 180.0f, 20.0f), "Render Offset", props.renderOffset);

					y += 45.0f;

					EditorGUI.BeginChangeCheck();
					props.sprite = (Sprite)EditorGUI.ObjectField(new Rect(x, y, 150.0f, 15.0f), props.sprite, typeof(Sprite), false);

					if (EditorGUI.EndChangeCheck())
					{
						if (props.hasCollider)
						{
							props.colliderSize.x = props.sprite.rect.width / props.sprite.pixelsPerUnit;
							props.colliderSize.y = props.sprite.rect.height / props.sprite.pixelsPerUnit;
						}
					}

					props.color = EditorGUI.ColorField(new Rect(x + 165.0f, y, 150.0f, 15.0f), props.color);
				}

				if (v % 2 == 0)
				{
					x += 400.0f;
					lastLeftY = y;

					if (v < td.variantCount - 1)
						y = startY;
					else y += 25.0f;
				}
				else
				{
					lastRightY = y;
					y = Max(lastLeftY, lastRightY) + 25.0f;
					x = 15.0f;
				}
			}
		}

		GUILayout.Space(y - 15.0f);
		EditorGUILayout.EndScrollView();
	}
}
