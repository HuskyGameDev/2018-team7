using UnityEditor;
using UnityEngine;

public static class MenuTools
{
	[MenuItem("Tools/Open Save Folder")]
	private static void OpenSaveFolder()
		=> EditorUtility.RevealInFinder(Application.persistentDataPath);
}
