using UnityEngine;
using System.Collections;
using AirAssault;

namespace AirAssault
{
	public class FinalBound : MonoBehaviour
	{
		void OnTriggerExit (Collider col)
		{
			if (col.gameObject.tag == "Player")
			{
				PlayerManager pm = col.gameObject.GetComponent<PlayerManager> ();
				pm.OnFinalBoundExit ();
			}
		}

	}
}