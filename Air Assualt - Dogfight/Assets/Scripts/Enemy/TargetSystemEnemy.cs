using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{

	public class TargetSystemEnemy : MonoBehaviour
	{
		public WeaponSystem weaponControl;
		public RadarSystemEnemy radarSystem;
		// holds targets detected by the radar
		public List<Target> potentialTargets;
		// holds targets that are in weapon range
		public List<Target> strikableTargets;
		public List<Target> lockingTargets;
		public List<Target> lockedTargets;
		public float countdownTime = 3.0f;
		public List<float> timers;

		// holds currently desired/focused targets
		public List<Target> currentTargets;

		// max num of simultaneous attack
		public int maxSimultaneousTarget = 10;
		public float lockDelay = 3f;
		public float lockDelta = 1f;

		void Awake ()
		{
			strikableTargets = new List<Target> ();
			lockingTargets = new List<Target> ();
			lockedTargets = new List<Target> ();
			currentTargets = new List<Target> ();
		}

		// Use this for initialization
		void Start ()
		{
			// connecting with core modules
			radarSystem = GetComponent<RadarSystemEnemy> ();
			weaponControl = GetComponent<WeaponSystem> ();

//		allTargets = radarSystem.allTargets;
			potentialTargets = radarSystem.potentialTargets;
		}
	
		// Update is called once per frame
		void Update ()
		{
			Scan ();
			AssignTarget ();

//		if (Input.GetButtonUp ("Cycle Target"))
//		{
//			cycleTargets ();
//		}
//		PrintTargets ();
//		PrintLockStatus ();
		}

		void FixedUpdate ()
		{

		}

		/*
	 * The role of this function is to assign current targets.
	 * We do not want to change currnet targets frequently as it will
	 * cause problem to lock on a specific target. If the current target
	 * is still in range then we must continue to track it. If it goes beyond range
	 * then we should wait for some time to check if it comes back in range. If
	 * it doesn't, then we must assign a new target for the player.
	 * We must ensure higher priority to those who are in weapon
	 * range
	 */
		public void AssignTarget ()
		{
			for (int i = 0; i < currentTargets.Count; i++)
			{
				if (!potentialTargets.Contains (currentTargets [i]))
				{
					currentTargets.RemoveAt (i);
				}
			}
        
			int count = maxSimultaneousTarget < potentialTargets.Count ? maxSimultaneousTarget : potentialTargets.Count;

			int j = 0;
			while (currentTargets.Count < count)
			{
				if (!currentTargets.Contains (potentialTargets [j]))
				{
					currentTargets.Add (potentialTargets [j++]);
				}
			}
		}

		void Scan ()
		{
			AcquireWeaponTargets ();
			AcquireTargetLocks ();
			ManageLockingTargets ();
			UpdateLockingStatus ();
			ManageLockedTargets ();
		}

		void AcquireWeaponTargets ()
		{
			strikableTargets.Clear ();

			for (int i = 0; i < potentialTargets.Count; i++)
			{
				if (potentialTargets [i].distance <= weaponControl.weaponRange && potentialTargets [i].angle <= weaponControl.weaponAngle)
				{
					strikableTargets.Add (potentialTargets [i]);
				}
			}
		}

		void AcquireTargetLocks ()
		{
			for (int i = 0; i < strikableTargets.Count; i++)
			{
				if (!lockedTargets.Contains (strikableTargets [i]) && !lockingTargets.Contains (strikableTargets [i]) && currentTargets.Contains (strikableTargets [i]))
				{
					lockingTargets.Add (strikableTargets [i]);
					timers.Add (countdownTime);
				}
			}
		}

		void ManageLockingTargets ()
		{
			for (int i = 0; i < lockingTargets.Count; i++)
			{
				if (!currentTargets.Contains (lockingTargets [i]) || !strikableTargets.Contains (lockingTargets [i]))
				{
					timers.RemoveAt (i);
					lockingTargets.RemoveAt (i);
				}
			}
		}

		void UpdateLockingStatus ()
		{
			// this loop may change the list size
			for (int i = 0; i < lockingTargets.Count; i++)
			{
				if (timers [i] <= 0f)
				{
					lockedTargets.Add (lockingTargets [i]);
					timers.RemoveAt (i);
					lockingTargets.RemoveAt (i);
				}
			}

			// two loops are required because the RemoveAt will change List Count
			// so we are first cleaning up the list then doing the updating
			for (int i = 0; i < lockingTargets.Count; i++)
			{
				timers [i] -= Time.deltaTime;
			}
		}

		void ManageLockedTargets ()
		{
			for (int i = 0; i < lockedTargets.Count; i++)
			{
				if (!currentTargets.Contains (lockedTargets [i]) || !strikableTargets.Contains (lockedTargets [i]))
				{
					lockedTargets.RemoveAt (i);
				}
			}
		}

		// for debug purpose
		void PrintTargets ()
		{
			Debug.Log ("Number of WEAPON targets = " + strikableTargets.Count);
			for (int i = 0; i < strikableTargets.Count; i++)
			{
				Debug.Log ("weaponTarget[" + i + "] = " + strikableTargets [i].targetObject.name);
			}

			Debug.Log ("Number of CURRENT targets = " + currentTargets.Count);
			for (int i = 0; i < currentTargets.Count; i++)
			{
				Debug.Log ("currentTarget[" + i + "] = " + currentTargets [i].targetObject.name);
			}
		}

		public void cycleUpTargets ()
		{
			if (currentTargets.Count > 0)
			{
				int index = potentialTargets.IndexOf (currentTargets [0]);
				index = ++index % potentialTargets.Count;
				currentTargets [0] = potentialTargets [index];
			}
		}

		public void cycleDownTargets ()
		{
			if (currentTargets.Count > 0)
			{
				int index = potentialTargets.IndexOf (currentTargets [0]);
				index = (--index + potentialTargets.Count) % potentialTargets.Count;
				currentTargets [0] = potentialTargets [index];
			}
		}

		void PrintLockStatus ()
		{
			for (int i = 0; i < lockingTargets.Count; i++)
			{
				Debug.Log (i + ") Locking === " + lockingTargets [i].targetObject.name);
				Debug.Log ("Countdown === " + timers [i]);
			}

			for (int i = 0; i < lockedTargets.Count; i++)
			{
				Debug.Log ("( " + i + " ) Locked " + lockedTargets [i].targetObject.name);
			}
		}
	}
}