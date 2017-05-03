using UnityEngine;
using System.Collections;

public class CrosshairControl : MonoBehaviour
{
	public RectTransform rectCrosshair;
	public Canvas uiWorldCanvas;
	public Transform crosshairWorldPosition;
	public Camera rendererCamera;

	// Use this for initialization
	void Start()
	{
		rectCrosshair = GetComponent<RectTransform>();
		rendererCamera = uiWorldCanvas.worldCamera;
		//rendererCamera = GetComponent<Canvas>().worldCamera;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 screenPosition = rendererCamera.WorldToScreenPoint(crosshairWorldPosition.position);
		rectCrosshair.anchoredPosition = new Vector2(screenPosition.x, screenPosition.y);
	}
}
