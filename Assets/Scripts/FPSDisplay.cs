using UnityEngine;
using System.Collections;
 
public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;
 
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
 
	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;
		Rect rect = new Rect(w/3f, 10, 300, 40);
 
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 30;
 
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Box(new Rect(w/3f, 0, 300, 55), "FPS");
		GUI.Label(rect, text, style);
	}
}