using UnityEngine;
using System;
using System.Collections;
using AirAssault;

namespace AirAssault
{
	[Serializable]
	public class AfterburnerControl : MonoBehaviour
	{
		public FlightControl flightControl;
		public ParticleSystem[] afterburners;
		public ParticleSystem[] glows;

		public float burnerMaxEmissionRate = 40f;
		public float burnerMaxSpeed = 8f;
		public float burnerMaxSize = 1.6f;
	
		public float glowMaxEmissionRate = 20f;
		public float glowMaxSpeed = 16f;

		// Use this for initialization
		void Start ()
		{
		
		}

		// Update is called once per frame
		void FixedUpdate ()
		{
			for (int i = 0; i < afterburners.Length; i++)
			{
				afterburners [i].emissionRate = flightControl.m_thrust * burnerMaxEmissionRate / flightControl.m_maxThrust;
				afterburners [i].startSpeed = flightControl.m_thrust * burnerMaxSpeed / flightControl.m_maxThrust;
				afterburners [i].startSize = flightControl.m_thrust * burnerMaxSize / flightControl.m_maxThrust;

				glows [i].emissionRate = flightControl.m_thrust * glowMaxEmissionRate / flightControl.m_maxThrust;
				glows [i].startSpeed = flightControl.m_thrust * glowMaxSpeed / flightControl.m_maxThrust;
			}
		}
	}
}