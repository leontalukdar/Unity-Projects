using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour
{
	public AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.Mouse0))
		{
			audioSource.Play();
		}
		else if(Input.GetKey(KeyCode.LeftControl))
		{
			audioSource.Play();
		}
	}
}
