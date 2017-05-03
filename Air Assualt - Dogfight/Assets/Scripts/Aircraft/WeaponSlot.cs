using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{

	[Serializable]
	public class WeaponSlot
	{
		public GameObject weapon { get; set; }

		public int capacity { get; set; }

		public WeaponSlot (GameObject weapon = null, int capacity = 0)
		{
			this.weapon = weapon;
			this.capacity = capacity;
		}
	}
}