using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{

	[Serializable]
	public class WeaponLoadout
	{
		private List<WeaponSlot> weaponSlots;

		public WeaponLoadout ()
		{
			weaponSlots = new List<WeaponSlot> ();
		}

		public WeaponLoadout (WeaponSlot[] weaponSlots)
		{
			this.weaponSlots = new List<WeaponSlot> (weaponSlots);
		}

		public WeaponLoadout (List<WeaponSlot> weaponSlots)
		{
			this.weaponSlots = new List<WeaponSlot> (weaponSlots);
		}

		/*
	 * Adds a weapon slot in the loadout.
	 * we want to add only one type of weapon in the slot.
	 * So we must check if the weapon type already exists before adding.
	 */
		public bool AddWeapon (GameObject weapon, int capacity)
		{
			int index = FindWeapon (weapon);

			if (index == -1)
			{
				weaponSlots.Add (new WeaponSlot (weapon, capacity));
				return true;
			}

			return false;
		}

		public bool RemoveWeapon (GameObject weapon)
		{
			int index = FindWeapon (weapon);

			if (index == -1)
			{
				return false;
			}

			weaponSlots.RemoveAt (index);
			return true;
		}

		public bool FillWeapon (GameObject weapon, int capacity)
		{
			int index = FindWeapon (weapon);

			if (index == -1)
			{
				return false;
			}

			weaponSlots [index].capacity = capacity;
			return true;
		}

		public int FindWeapon (GameObject weapon)
		{
			for (int i = 0; i < weaponSlots.Count; i++)
			{
				if (weaponSlots [i].weapon == weapon)
				{
					return i;
				}
			}

			return -1;
		}

		public GameObject GetWeaponAt (int index)
		{
			return weaponSlots [index].weapon;
		}

		public WeaponSlot GetAt (int index)
		{
			return weaponSlots [index];
		}

		public int Count ()
		{
			return weaponSlots.Count;
		}

		public void Clear ()
		{
			weaponSlots.Clear ();
		}
	}
}