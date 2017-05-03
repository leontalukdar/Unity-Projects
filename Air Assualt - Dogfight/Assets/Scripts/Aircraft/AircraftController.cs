using UnityEngine;
using System;
using System.Collections;
using AirAssault;

namespace AirAssault
{
	[Serializable]
	public class AircraftController : MonoBehaviour
	{
		public FlightControl flightControl;
		public RadarSystem radarSystem;
		public TargetSystem targetSystem;
		public WeaponSystem weaponSystem;
		public CounterMeasureSystem cmSystem;

		public float inputAxis;

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
			flightControl.isBraking = false;

			// thrust and brake are mutually exclusive
			if (Input.GetButton ("Brake"))
			{
				flightControl.Brake ();
			}

			if (!flightControl.isBraking)
			{
				inputAxis = Input.GetAxis ("Thrust");
				flightControl.Thrust (inputAxis);
			}

			// roll, pitch and yaw are not mutually exclusive
			inputAxis = Input.GetAxis ("Roll");
			flightControl.Roll (inputAxis);

			inputAxis = Input.GetAxis ("Pitch");
			flightControl.Pitch (inputAxis);

			inputAxis = Input.GetAxis ("Yaw");
			flightControl.Yaw (inputAxis);



			/*********** WEAPON SYSTEM ***************/

			if (Input.GetButtonUp ("Fire Weapon"))
			{
				weaponSystem.FireWeapon ();
			}

			inputAxis = Input.GetAxis ("Cycle Weapon");
			if (inputAxis > 0f)
			{
				weaponSystem.CycleUpWeapon ();
			}
			else if (inputAxis < 0f)
			{
				weaponSystem.CycleDownWeapon ();
			}


			/************* TARGET SYSTEM ***************/

			inputAxis = Input.GetAxis ("Cycle Target");
			if (inputAxis > 0f)
			{
				targetSystem.cycleUpTargets ();
			}
			else if (inputAxis < 0f)
			{
				targetSystem.cycleDownTargets ();
			}


			/********* COUNTER-MEASURE SYSTEM **********/

			if (Input.GetButtonUp ("Deploy Flare"))
			{
				cmSystem.DeployFlare ();
			}
		}
	}
}