using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{

	public class CounterMeasureSystemAI : MonoBehaviour
	{
		public List<GameObject> targettedBy;
		public Transform cmBay;
		public GameObject flare;
		public GameObject jammer;
		public int flareCount;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
		}

		void DeployFlare ()
		{
			if (flareCount > 0)
			{
				GameObject flareClone = (GameObject)Instantiate (flare, cmBay.position, cmBay.rotation);
				flareCount--;
			}
		}
	}
}