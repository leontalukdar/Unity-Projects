using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CameraControl : MonoBehaviour
{
	public GameObject[] cameras;
	public int current = 0;

	// Use this for initialization
	void Start ()
	{
		DisableAll ();
		ActivateCamera ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonUp ("Cycle Camera"))
		{
			DisableAll ();
			CycleCamera ();
			ActivateCamera ();
		}
	}

	void CycleCamera ()
	{
		current = ++current % cameras.Length;
	}

	void DisableAll ()
	{
		for (int i = 0; i < cameras.Length; i++)
		{
			cameras [i].SetActive (false);
		}
	}

	void ActivateCamera ()
	{
		cameras [current].SetActive (true);
	}
}
