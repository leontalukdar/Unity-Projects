using UnityEngine;
using System.Collections;
using AirAssault;

namespace AirAssault
{
	public class MissileControl : MonoBehaviour
	{
		public GameObject host;
		public GameObject target;
		public Rigidbody m_rigidbody;
		public AudioSource audioSource;
		public GameObject explosion;

		public float speed;
		public float maxSpeed = 738f;
		public float acceleration = 98f;
		public float turnFactor = 1f;

		//	public float force = 100f;
		//	public float forceFactor = .1f;

		public float effectiveAngle = 60f;
		public float effectiveRange = 50f;
		public float fuseDelay = 2f;
		public float fuel = 10f;
		public float fuelFactor = 5f;
		public float cmDetectionProbability = .1f;

		void Awake ()
		{
        
		}

		// Use this for initialization
		void Start ()
		{
			gameObject.tag = "Missile";
			m_rigidbody = GetComponent<Rigidbody> ();
			audioSource = gameObject.GetComponent<AudioSource> ();
			audioSource.Play ();

			speed = Vector3.Magnitude (host.GetComponent<Rigidbody> ().velocity);
			Thrust ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			FuelManager ();
			TrackTarget ();
			Thrust ();
		}

		void FixedUpdate ()
		{

		}

		void TrackTarget ()
		{
			if (target)
			{
				Quaternion currentRotation = Quaternion.LookRotation (transform.forward);
				Quaternion targetRotation = Quaternion.LookRotation (target.transform.position - transform.position);
				float angle = Mathf.Abs (Quaternion.Angle (currentRotation, targetRotation));

				if (angle > effectiveAngle)
				{
					target = null;
				}

				Quaternion newRotation = Quaternion.RotateTowards (currentRotation, targetRotation, turnFactor);

				m_rigidbody.MoveRotation (newRotation);
			}
		}

		void Thrust ()
		{
			m_rigidbody.velocity = transform.forward * CalculateVelocity ();
		}

		float CalculateVelocity ()
		{
			speed += acceleration * Time.deltaTime;
			speed = Mathf.Clamp (speed, 0f, maxSpeed);
			return speed;
		}

		void FuelManager ()
		{
			fuel -= fuelFactor * Time.deltaTime;

			if (fuel <= 0f)
			{
				Detonate ();
			}
		}

		void Detonate ()
		{
			Instantiate (explosion, transform.position, RandomRotation ());
			Destroy (gameObject);
		}

		void OnTriggerEnter (Collider col)
		{
			Debug.Log ("Missile collided with " + col.gameObject.name);
			if (col.gameObject == target)
			{
				if (col.gameObject.tag == "Player")
				{
					Debug.Log ("Player");
					
					col.gameObject.GetComponent<PlayerManager> ().OnDamage (50f);
				}
				else if (col.gameObject.tag == "Enemy")
				{
					Debug.Log ("Player");
					col.gameObject.GetComponent<EnemyAI> ().OnDeath ();
					host.GetComponent<PlayerManager> ().OnUnitKill ();
				}

				Detonate ();
			}
		}

		Quaternion RandomRotation ()
		{
			float x = Random.Range (0f, 360f);
			float y = Random.Range (0f, 360f);
			float z = Random.Range (0f, 360f);

			return Quaternion.Euler (new Vector3 (x, y, z));
		}
	}
}