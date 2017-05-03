using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{

	[Serializable]
	public class EnemyAI : MonoBehaviour
	{
		public GameObject explosionPrefab;
		public GameObject missile;
		public GameObject missilePrefab;
		public MissileControl missileControl;
		public RadarSystemEnemy radarSystem;
		public TargetSystemEnemy targetSystem;
		public WeaponSystem weaponSystem;

		public Transform horizontalAxis;
		public Transform verticalAxis;

		public float coolDownTime = 5f;
		public int missileCount = 999;

		public bool cooledDown = true;
		public bool freeRoamMode = true;
		public bool engageMode = false;

		public float health = 100;

		// Use this for initialization
		void Start ()
		{
			radarSystem = GetComponent<RadarSystemEnemy> ();
			targetSystem = GetComponent<TargetSystemEnemy> ();
			weaponSystem = GetComponent<WeaponSystem> ();

			weaponSystem.weaponLoadout.AddWeapon (missilePrefab, missileCount);
		}
	
		// Update is called once per frame
		void Update ()
		{
			Scan ();

			if (Input.GetButtonUp ("Fire Weapon"))
			{
				FireMissile (null);
			}
		}

		void Scan ()
		{
			if (targetSystem.strikableTargets.Count > 0)
			{
				if (cooledDown)
				{
					Engage ();
				}
			}
			else
			{
				FreeRoam ();
			}
		}

		void Engage ()
		{
			engageMode = true;
			freeRoamMode = false;

//		Track ();

			for (int i = 0; i < targetSystem.lockedTargets.Count; i++)
			{
				FireMissile (targetSystem.lockedTargets [i].targetObject);
			}

			StartCoroutine (CoolDown ());
		}

		void FireMissile (GameObject target)
		{
			missile = (GameObject)Instantiate (weaponSystem.currentSlot.weapon, weaponSystem.weaponBay.position, weaponSystem.weaponBay.rotation);
			missileControl = missile.GetComponent<MissileControl> ();
			missileControl.host = this.gameObject;
			missileControl.target = target;
		}

		void FreeRoam ()
		{
			freeRoamMode = true;
			engageMode = false;
		}

		IEnumerator CoolDown ()
		{
			cooledDown = false;

			yield return new WaitForSeconds (coolDownTime);

			cooledDown = true;
		}

		void Track ()
		{
			if (targetSystem.strikableTargets.Count > 0)
			{
//			Transform target = targetSystem.currentTargets [0];
			}
		}

		public void OnDeath ()
		{
			Instantiate (explosionPrefab, transform.position, transform.rotation);
			//Destroy (this.gameObject);
			gameObject.SetActive (false);
		}
	}
}