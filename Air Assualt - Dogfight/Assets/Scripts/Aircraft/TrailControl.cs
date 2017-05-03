using UnityEngine;
using System.Collections;
using AirAssault;

namespace AirAssault
{

	public class TrailControl : MonoBehaviour
	{
		public FlightControl flightControl;
		public TrailRenderer[] jetTrails;
		public float trailVelocity = 100f;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
			for (int i = 0; i < jetTrails.Length; i++)
			{
				if (flightControl.m_velocity > trailVelocity)
				{
					jetTrails [i].enabled = true;
				}
				else
				{
					jetTrails [i].enabled = false;
				}
			}
		}
	}
}
