using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

public class WarningSystem : MonoBehaviour
{
	public Dictionary<GameObject, bool> lockedOnBy;
	public List<GameObject> incomingMissiles;


	void Awake ()
	{
		lockedOnBy = new Dictionary<GameObject, bool> ();
		incomingMissiles = new List<GameObject> ();
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Scan ();
//        Print();
	}

	void Scan ()
	{
		SearchIncomingMissiles ();
	}

	void SearchIncomingMissiles ()
	{
		incomingMissiles.Clear ();

		GameObject[] missiles = GameObject.FindGameObjectsWithTag ("Missile");
		MissileControl mc;

		for (int i = 0; i < missiles.Length; i++)
		{
			mc = missiles [i].GetComponent<MissileControl> ();

			if (mc.target == this.gameObject)
			{
				incomingMissiles.Add (missiles [i]);
			}
		}
	}

	void Print ()
	{
		for (int i = 0; i < incomingMissiles.Count; i++)
		{
			Debug.Log ("Missile " + incomingMissiles [i].name + " incoming");
		}
	}
}
