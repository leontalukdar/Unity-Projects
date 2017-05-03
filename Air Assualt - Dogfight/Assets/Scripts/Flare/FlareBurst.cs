using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AirAssault;


public class FlareBurst : MonoBehaviour
{
	public GameObject flare;
	public List<GameObject> flares;
	public int burstSize = 10;
	public float maxSpreadDelta = 10f;
	public float maxDelay = 10f;
	public bool deployed = false;
	public float successProbability = 0.7f;
	public float distractRadius = 100f;

	// Use this for initialization
	void Start ()
	{
		flares = new List<GameObject> ();
		GameObject flareClone;
		float x;
		float y;
		float z;
		float t;
		Vector3 pos;

		for (int i = 0; i < burstSize; i++)
		{
			x = transform.position.x + Random.Range (-maxSpreadDelta, maxSpreadDelta);
			y = transform.position.y + Random.Range (-maxSpreadDelta, maxSpreadDelta);
			z = transform.position.z + Random.Range (-maxSpreadDelta, maxSpreadDelta);
			t = Random.Range (0f, maxDelay);
			pos = new Vector3 (x, y, z);

			flareClone = (GameObject)Instantiate (flare, pos, transform.rotation);
			flareClone.transform.parent = transform;
			flareClone.name = "Flare " + i;
			flareClone.tag = "Flare";
			flares.Add (flareClone);
		}

		deployed = true;
	}

	// Update is called once per frame
	void Update ()
	{
		flares.RemoveAll (item => item == null);

		if (flares.Count == 0 && deployed)
		{
			OnDeath ();
		}

		DistractMissiles ();
	}

	void DistractMissiles ()
	{
		GameObject[] missiles = GameObject.FindGameObjectsWithTag ("Missile");
		MissileControl missileControl;

		for (int i = 0; i < missiles.Length; i++)
		{
			if (Vector3.Distance (transform.position, missiles [i].transform.position) < distractRadius)
			{
				missileControl = missiles [i].GetComponent<MissileControl> ();

				if (Random.Range (0f, 1f) < successProbability)
				{
					if (Random.Range (0f, 1f) > missileControl.cmDetectionProbability)
					{
						missileControl.target = gameObject;
					}
				}
			}
		}
	}

	void OnDeath ()
	{
		Destroy (gameObject);
	}
}
