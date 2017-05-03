using UnityEngine;
using System;
using System.Collections;
using AirAssault;

namespace AirAssault
{
	[Serializable]
	public class WarningBound : MonoBehaviour
	{
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerExit (Collider col)
		{
			if (col.gameObject.tag == "Player")
			{
				PrintExit (col.gameObject);

				PlayerManager pm = col.gameObject.GetComponent<PlayerManager> ();
				pm.OnWarnBoundExit ();
			}
		}

		void OnTriggerEnter (Collider col)
		{
			if (col.gameObject.tag == "Player")
			{
				PrintEnter (col.gameObject);

				PlayerManager pm = col.gameObject.GetComponent<PlayerManager> ();
				pm.OnWarnBoundEnter ();
			}
		}

		void PrintEnter (GameObject go)
		{
			Debug.Log (go.name + " entered.");
		}

		void PrintExit (GameObject go)
		{
			Debug.Log (go.name + " exited.");
		}
	}
}