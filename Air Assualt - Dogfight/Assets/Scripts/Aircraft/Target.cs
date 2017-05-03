using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{
	public class Target
	{
		public GameObject targetObject;
		public float distance;
		public float angle;

		public Target (GameObject go, float distance = Mathf.Infinity, float angle = Mathf.Infinity)
		{
			targetObject = go;
			this.distance = distance;
			this.angle = angle;
		}

		public static implicit operator bool (Target target)
		{
			if (target != null)
			{
				if (target.targetObject != null)
				{
					return true;
				}
			}

			return false;
		}
	}
}