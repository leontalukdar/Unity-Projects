using UnityEngine;
using System;
using System.Collections;
using AirAssault;

namespace AirAssault
{

	public class MachineGunControl : MonoBehaviour
	{
		public WeaponSystem weaponControl;
		public Bullet bullet;
		ParticleSystem bullets;
		// particle system that generates bullets
		public int ammoCount = 5000;
		public float inputAxis;
		// Rate of fire per second
		public int rateOfFire = 100;
		public bool firing = false;
		public bool playingAudio = false;
		public AudioSource audioSource;

		// Use this for initialization
		void Start ()
		{
			bullet = new Bullet (.1f, 350f, 1000f);
			bullets = GetComponent<ParticleSystem> ();
			audioSource = GetComponent<AudioSource> ();

//		ammoCount = weaponControl.ammo;
		}
	
		// Update is called once per frame
		void Update ()
		{
			firing = false;

			if (Input.GetButton ("Fire Gun"))
			{
				Fire ();
			}

			if (firing)
			{
				PlayAudio ();
				bullets.Play ();
			}
			else
			{
				StopAudio ();
				bullets.Stop ();
			}
		}

		void Fire ()
		{
			if (ammoCount > 0)
			{
				firing = true;
				int bulletsFired = (int)(rateOfFire * Time.deltaTime);
				ammoCount -= bulletsFired;
				ammoCount = (int)Mathf.Clamp (ammoCount, 0f, Mathf.Infinity);

				Ray bulletsRay = new Ray (transform.position + transform.forward * 2, transform.forward);
				RaycastHit bulletsHit;

				if (Physics.Raycast (bulletsRay, out bulletsHit, bullet.effectiveRange))
				{
					Debug.Log ("Bullet hit : " + bulletsHit.collider.gameObject.name);
					if (bulletsHit.collider.gameObject.tag == "Player")
					{
						PlayerManager pm = bulletsHit.collider.gameObject.GetComponent<PlayerManager> ();
						pm.OnDamage (bullet.damage * bulletsFired);
					}
				}
			}

		}

		void FillAmmo (int ammoCount)
		{
			this.ammoCount = ammoCount;
		}

		void PlayAudio ()
		{
			playingAudio = true;
			audioSource.Play ();
		}

		void StopAudio ()
		{
			playingAudio = false;
			audioSource.Stop ();
		}
	}
}