using UnityEngine;
using System;
using System.Collections;
using AirAssault;

namespace AirAssault
{

	public enum Status
	{
		ENGAGE_MODE,
		EVADE_MODE,
		FREE_ROAM
	}

	[Serializable]
	public class AircraftControllerAI : MonoBehaviour
	{
		public FlightControl flightControl;
		public RadarSystem radarSystem;
		public TargetSystem targetSystem;
		public WeaponSystem weaponSystem;
		public CounterMeasureSystem cmSystem;
		public WarningSystem warningSystem;

		public Status status = new Status ();

		void Awake ()
		{
			status = Status.FREE_ROAM;
		}


		// Use this for initialization
		void Start ()
		{
			flightControl = GetComponent<FlightControl> ();
			radarSystem = GetComponent<RadarSystem> ();
			targetSystem = GetComponent<TargetSystem> ();
			weaponSystem = GetComponent<WeaponSystem> ();
			cmSystem = GetComponent<CounterMeasureSystem> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			AcquireTarget ();
			EngageTarget ();
	
		}

		void AcquireTarget ()
		{
			// if there is no target selected
			if (targetSystem.currentTargets.Count < 0)
			{
				// is there any target out there
				if (targetSystem.potentialTargets.Count > 0)
				{
					targetSystem.cycleUpTargets ();
				}
				else
				{
					status = Status.FREE_ROAM;
					return;
				}
			}

			status = Status.ENGAGE_MODE;
		}

		void EngageTarget ()
		{
			if (status != Status.ENGAGE_MODE)
			{
				return;
			}

		}
	}
}