using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{

	public class TargetSystemUI : MonoBehaviour
	{
		public GameObject uiDetection;
		public Dictionary<Target, GameObject> uiDetections;
		public RadarSystem radarSystem;
		public TargetSystem targetSystem;
		public List<Target> potentialTargets;
		public List<Target> currentTargets;
		public List<Target> lockingTargets;
		public List<Target> lockedTargets;
		public Canvas uiCanvas;
		public int maxSimultaneousTarget;

		public 
    void Awake ()
		{
			// allocating space for lists
			uiDetections = new Dictionary<Target, GameObject> ();
		}


		// Use this for initialization
		void Start ()
		{
			// connecting to the core systems
			radarSystem = GetComponent<RadarSystem> ();
			targetSystem = GetComponent<TargetSystem> ();
			
			//getting access to the radar detection
			// requires connection to the core systems first
			potentialTargets = radarSystem.potentialTargets;
			currentTargets = targetSystem.currentTargets;
			lockingTargets = targetSystem.lockingTargets;
			lockedTargets = targetSystem.lockedTargets;

			// maximum number of radar detection and weapon detections. These are acquired
			// from the target system module
			maxSimultaneousTarget = targetSystem.maxSimultaneousTarget;
		}

		// Update is called once per frame
		void Update ()
		{
			Clean ();
			Prepare ();
			UpdateDetections ();
			UpdateLocks ();
			UpdateCurrentTargets ();
//		Print();
		}

		void UpdateDetections ()
		{
			for (int i = 0; i < potentialTargets.Count; i++)
			{
				Calculate (potentialTargets [i]);
			}
		}

		void Calculate (Target target)
		{
			Vector3 targetPosition = target.targetObject.transform.position;
			Vector3 targetDirection = targetPosition - transform.position;
			float angle = Vector3.Angle (targetDirection, uiCanvas.worldCamera.transform.forward);

			// checking if the detected object is behind the aircraft 
			if (angle >= 120f)
			{
				uiDetections [target].SetActive (false);
				return;
			}
			Vector3 screenPosition = uiCanvas.worldCamera.WorldToScreenPoint (targetPosition);
			Vector2 rectPosition = new Vector2 (screenPosition.x, screenPosition.y);
			RectTransform rect = uiDetections [target].GetComponent<RectTransform> ();
			rect.anchoredPosition = rectPosition;
			rect.localRotation = Quaternion.identity;

			uiDetections [target].SetActive (true);
		}


		void Prepare ()
		{
			for (int i = 0; i < potentialTargets.Count; i++)
			{
				if (!uiDetections.ContainsKey (potentialTargets [i]))
				{
					GameObject go = (GameObject)Instantiate (uiDetection, uiDetection.transform.position, uiDetection.transform.rotation);
					RectTransform rect = go.GetComponent<RectTransform> ();
					rect.SetParent (uiCanvas.GetComponent<RectTransform> ());
					rect.localPosition = Vector3.zero;
					rect.localRotation = Quaternion.identity;
					rect.localScale = new Vector3 (1f, 1f, 1f);
					go.SetActive (false);
					go.name = "Detection " + i;
					go.GetComponent<RadarObject> ().target = potentialTargets [i];
					uiDetections.Add (potentialTargets [i], go);
				}
			}
		}

		void UpdateLocks ()
		{
			for (int i = 0; i < potentialTargets.Count; i++)
			{
				RadarObject ro = uiDetections [potentialTargets [i]].GetComponent<RadarObject> ();

				if (lockedTargets.Contains (potentialTargets [i]))
				{
					ro.Lock ();
				}
				else if (lockingTargets.Contains (potentialTargets [i]))
				{
					if (!ro.blink)
					{
						ro.BlinkOn ();
					}
				}
				else
				{
					ro.Reset ();
				}
			}
		}

		void UpdateCurrentTargets ()
		{
			RadarObject ro;

			for (int i = 0; i < currentTargets.Count; i++)
			{
				ro = uiDetections [currentTargets [i]].GetComponent<RadarObject> ();
				ro.Focus ();
			}
		}

		void Clean ()
		{
			List<Target> obsoleteTargets = uiDetections.Keys.Except (potentialTargets).ToList ();

			for (int i = 0; i < obsoleteTargets.Count; i++)
			{
				Destroy (uiDetections [obsoleteTargets [i]]);
				uiDetections.Remove (obsoleteTargets [i]);
			}
		}
	}
}