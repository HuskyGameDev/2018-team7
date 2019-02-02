using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
	void Start()
	{
		if (Screen.width != 1024 || Screen.height != 576)
			Screen.SetResolution(1024, 576, false);
	}
}
