using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AirAssault;

namespace AirAssault
{

	public class uiAircraftControl : MonoBehaviour
	{
		public GameObject host;
		public GameObject machinegun;
		public WeaponSystem weaponSystem;
		public MachineGunControl machineGunControl;
		public CounterMeasureSystem cmSystem;

		public Text machineGunCount;

		public Text missileLabel;
		public Text missileCount;
		public Image missileIcon1;
		public Image missileIcon2;

		public Text cmLabel;
		public Text cmCount;

		void Awake ()
		{

		}

		// Use this for initialization
		void Start ()
		{
			weaponSystem = host.GetComponent<WeaponSystem> ();
			machineGunControl = machinegun.GetComponent<MachineGunControl> ();
			cmSystem = host.GetComponent<CounterMeasureSystem> ();
		}

		// Update is called once per frame
		void Update ()
		{
			if (machineGunControl != null)
			{
				machineGunCount.text = "" + machineGunControl.ammoCount;
			}
			else
			{
				machineGunCount.text = "No gun attached";
			}

			//missileLabel.text = weaponControl.currentSlot.weapon.name;
			if (weaponSystem.currentSlot != null)
			{
				missileCount.text = "" + weaponSystem.currentSlot.capacity;
			}
			else
			{
				missileCount.text = "No missile";
			}
//        cmLabel = "Flare";
			cmCount.text = "" + cmSystem.flareCount;
		}
	}
}