using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{

	[Serializable]
	public class RadarSystemEnemy : MonoBehaviour
	{
		public List<GameObject> allplayers;

		// holds all targets in the game
		public List<Target> allTargets;
		public List<Target> potentialTargets;
		// holds radar detected targets

		public float radarRange = 50000f;


		void Awake ()
		{
			allplayers = new List<GameObject> ();
			allTargets = new List<Target> ();
			potentialTargets = new List<Target> ();
		}

		// Use this for initialization
		void Start ()
		{
			UpdatePlayers ();
			UpdateTargets ();
		}

		// Update is called once per frame
		void Update ()
		{
			Scan ();
//		PrintTargets ();
		}

		void Scan ()
		{
			potentialTargets.Clear ();
			UpdatePlayers ();
			UpdateTargets ();

			// Collecting all targets in range of the radar
			for (int i = 0; i < allTargets.Count; i++)
			{
				if (allTargets [i].distance < radarRange)
				{
					potentialTargets.Add (allTargets [i]);
				}
			}
		}

		void UpdatePlayers ()
		{
			GameObject[] temp = GameObject.FindGameObjectsWithTag ("Player");
			float distance;
			float angle;

			for (int i = 0; i < temp.Length; i++)
			{
				if (temp [i] == this.gameObject)
				{
					continue;
				}

				if (!allplayers.Contains (temp [i]))
				{
					distance = CalculateDistance (temp [i]);
					angle = CalculateAngle (temp [i]);
					allTargets.Add (new Target (temp [i], distance, angle));
					allplayers.Add (temp [i]);
				}
			}
		}

		void UpdateTargets ()
		{
			for (int i = 0; i < allTargets.Count; i++)
			{
				allTargets [i].distance = CalculateDistance (allTargets [i].targetObject);
				allTargets [i].angle = CalculateAngle (allTargets [i].targetObject);
			}
		}

		float CalculateDistance (GameObject go)
		{
			return Vector3.Distance (transform.position, go.transform.position);
		}

		float CalculateAngle (GameObject go)
		{
			Quaternion currentRotation = Quaternion.LookRotation (transform.forward);
			Quaternion targetRotation = Quaternion.LookRotation (go.transform.position - transform.position);
			return Mathf.Abs (Quaternion.Angle (currentRotation, targetRotation));
		}


		void PrintTargets ()
		{
			Debug.Log ("ALL TARGETS : " + allTargets.Count);
			for (int i = 0; i < allTargets.Count; i++)
			{
				Debug.Log (allTargets [i].targetObject.name);
			}

			Debug.Log ("RADAR TARGETS : " + potentialTargets.Count);
			for (int i = 0; i < potentialTargets.Count; i++)
			{
				Debug.Log (potentialTargets [i].targetObject.name);
			}
		}
	}
}