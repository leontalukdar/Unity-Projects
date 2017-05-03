using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using AirAssault;

namespace AirAssault
{
	[Serializable]
	public class PlayerManager : MonoBehaviour
	{
		public GameObject[] model;
		public FlightControl flightControl;
		public AircraftController aircraftController;
		public uiHUDControl playerHUD;
		public GameObject explosion;
		public float health = 100f;
		public int score = 0;
		public int unitsKilled = 0;

		public int baseUnitValue = 1000;
		public int roundModifier = 1;

		public bool alive = true;

		void Start ()
		{
			flightControl = GetComponent<FlightControl> ();
			aircraftController = GetComponent<AircraftController> ();
			ResetPlayer ();
		}

		void Update ()
		{
//			if (flightControl.m_altitude < 50f)
//			{
//				OnDeath ();
//			}
		}

		public void ResetPlayer ()
		{
		
		}

		public void EnablePlayer ()
		{
			aircraftController.enabled = true;
		}

		public void DisablePlayer ()
		{
			aircraftController.enabled = false;
		}

		public void OnDamage (float damage)
		{
			health -= damage;

			if (health <= 0f)
			{
				OnDeath ();
			}
		}

		public void OnUnitKill ()
		{
			unitsKilled++;
			score += baseUnitValue * roundModifier;
		}

		void OnDeath ()
		{
			Hide ();
			Instantiate (explosion, transform.position, transform.rotation);
			alive = false;
			DisablePlayer ();
		}

		public void OnWarnBoundExit ()
		{
			string message = "Please turn back. You are leaving game zone.";
			playerHUD.DisplayWarning (message);
		}

		public void OnWarnBoundEnter ()
		{
			playerHUD.DisableWarning ();
		}

		public void OnFinalBoundExit ()
		{
			OnDeath ();
		}

		void OnCollisionEnter (Collision col)
		{
			Debug.Log (gameObject.name + "had a collision with " + col.gameObject.name);
			OnDeath ();
		}

		void Hide ()
		{
			for (int i = 0; i < model.Length; i++)
			{
				model [i].SetActive (false);
			}
		}
	}
}