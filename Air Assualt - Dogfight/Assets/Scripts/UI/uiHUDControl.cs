using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AirAssault;

namespace AirAssault
{
	public class uiHUDControl : MonoBehaviour
	{
		public GameObject host;
		public FlightControl flightControl;
		public Text altitude;
		public Text speed;
		public Text message;

		// Use this for initialization
		void Start ()
		{
			// connecting to core modules
			flightControl = host.GetComponent<FlightControl> ();
		}

		// Update is called once per frame
		void Update ()
		{
			altitude.text = "" + (int)flightControl.m_altitude + " m";
			speed.text = "" + (int)(flightControl.m_velocity * 3600 / 1000) + " Km/h";
		}

		public void DisplayWarning (string msg)
		{
			message.enabled = true;
			message.text = msg;
		}

		public void DisableWarning ()
		{
			message.text = "";
			message.enabled = false;
		}
	}
}