using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CanvasControl : MonoBehaviour
{
	public CameraControl cameraControl;
	private Canvas canvas;

	// Use this for initialization
	void Start ()
	{
		canvas = GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		int index = cameraControl.current;
		canvas.worldCamera = cameraControl.cameras [index].GetComponent<Camera> ();
	}
}
