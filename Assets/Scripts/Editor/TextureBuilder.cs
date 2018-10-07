using UnityEngine;
using UnityEditor;
using System.IO;
using System;

// Helper class to generate simple textures. Other patterns could be added in here.
// This is temporary until we get art.
public sealed class TextureBuilder : EditorWindow
{
	private string lightName;
	private int radius;
	private float intensity;

	private string flatName;
	private Color flatColor;
	private int flatWidth, flatHeight;

	[MenuItem("Tools/Texture Builder")]
	public static void OpenBuilder()
	{
		GetWindow(typeof(TextureBuilder), false, "Texture Builder", true);
	}

	private void DrawDivider()
	{
		GUILayout.Box(String.Empty, GUILayout.ExpandWidth(true), GUILayout.Height(4));
	}

	private void OnGUI()
	{
		EditorGUILayout.Space();
		DrawDivider();
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Flat Texture");
		EditorGUILayout.Space();

		flatName = EditorGUILayout.TextField("Name", flatName);
		EditorGUILayout.Space();

		flatWidth = EditorGUILayout.IntField("Width", flatWidth);
		flatHeight = EditorGUILayout.IntField("Height", flatHeight);
		EditorGUILayout.Space();

		flatColor = EditorGUILayout.ColorField("Color", flatColor);
		EditorGUILayout.Space();

		if (GUILayout.Button("Generate"))
			FlatColor();
	}

	private void FlatColor()
	{
		Texture2D tex = new Texture2D(flatWidth, flatHeight);

		for (int y = 0; y < tex.height; y++)
		{
			for (int x = 0; x < tex.width; x++)
				tex.SetPixel(x, y, flatColor);
		}

		string path = Application.dataPath + "/Sprites/" + flatName + ".png";
		File.WriteAllBytes(path, tex.EncodeToPNG());
		AssetDatabase.Refresh();
	}
}