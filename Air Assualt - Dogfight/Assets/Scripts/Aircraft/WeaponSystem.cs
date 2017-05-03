using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{

	[Serializable]
	public class WeaponSystem : MonoBehaviour
	{
		public WeaponLoadout weaponLoadout;
		public GameObject machineGun;

		public WeaponSlot currentSlot;
		public int slotIndex = 0;

		public MachineGunControl machineGunControl;
		public MissileControl missileControl;
		public TargetSystem targetSystem;
		public TargetSystemUI targetSystemUI;
		public Transform weaponBay;

		public GameObject missilePrefab;

		public float weaponRange;
		public float weaponAngle;
		public float pushForce = 1000f;
		// force applied to missile when launched from weapon bay

		// Use this for initialization
		void Start ()
		{
			targetSystem = GetComponent<TargetSystem> ();
			targetSystemUI = GetComponent<TargetSystemUI> ();
			machineGunControl = machineGun.GetComponent<MachineGunControl> ();
			weaponLoadout.AddWeapon (missilePrefab, 999);
			//weaponLoadout = GameObject.Find("GameManager").GetComponent<GameManager>().weaponLoadout;
		}
	
		// Update is called once per frame
		void Update ()
		{
			UpdateCurrentWeapon ();
		
//		if (Input.GetButtonUp ("Fire Weapon"))
//		{
//			FireWeapon ();
//		}
		}

		public void FireWeapon ()
		{
			int count = targetSystemUI.lockedTargets.Count > 1 ? targetSystemUI.lockedTargets.Count : 1;

			for (int i = 0; i < count; i++)
			{
				GameObject missileClone = (GameObject)Instantiate (currentSlot.weapon, weaponBay.position, weaponBay.rotation);
			
				MissileControl mc = missileClone.GetComponent<MissileControl> ();
				mc.host = gameObject;
				mc.target = targetSystemUI.lockedTargets.Count > 0 ? targetSystemUI.lockedTargets [i].targetObject : null;
				currentSlot.capacity--;
			}
		}

		public void CycleUpWeapon ()
		{
			slotIndex = ++slotIndex % weaponLoadout.Count ();
		}

		public void CycleDownWeapon ()
		{
			slotIndex = (--slotIndex + weaponLoadout.Count ()) % weaponLoadout.Count ();
		}

	
		//Collects the specs of the currently loaded missile
		void UpdateCurrentWeapon ()
		{
			if (weaponLoadout != null)
			{
				currentSlot = weaponLoadout.GetAt (slotIndex);
				missileControl = currentSlot.weapon.GetComponent<MissileControl> ();
				weaponRange = missileControl.effectiveRange;
				weaponAngle = missileControl.effectiveAngle;
			}
		}
	}
}