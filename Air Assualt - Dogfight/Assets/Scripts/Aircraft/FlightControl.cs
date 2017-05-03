using UnityEngine;
using System.Collections;
using AirAssault;

namespace AirAssault
{
	public class FlightControl : MonoBehaviour
	{
		public float m_maxThrust = 100f;
		public float m_thrust;
		public float m_thrustFactor = 1f;
		public float m_maxAcceleration = 1.77f;
		public float m_acceleration;
		public float m_velocity = 0f;
		public float m_maxVelocity = 475f;
		public float m_stallVelocity = 20f;
		public float m_rollFactor = 1f;
		public float m_pitchFactor = 1f;
		public float m_yawFactor = 1f;
		public float m_brakeForce = 500f;
		public float m_dragFactor;
		public float m_angularDragFactor;
		public float m_maxAltitude;
		public float m_altitude;
		public float m_rateOfClimb;

		public float m_drag;
		public float m_angularDrag;

		public float inputAxis;

		public bool isBraking;

		public Rigidbody m_rigidbody;

		// Use this for initialization
		void Start ()
		{
			m_rigidbody = GetComponent<Rigidbody> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			//isBraking = false;

//		if (Input.GetButton ("Brake"))
//		{
//			Brake ();
//		}
//
//		if (!isBraking)
//		{
//			Thrust ();
//		}
//
//		Roll ();
//		Pitch ();
//		Yaw ();
			Move ();
		}

		void FixedUpdate ()
		{

		}

		public void Thrust (float inputAxis)
		{
			m_thrust += inputAxis * m_thrustFactor;
			m_thrust = Mathf.Clamp (m_thrust, 0f, m_maxThrust);

		}

		public void Brake ()
		{
			isBraking = true;
			//m_thrust -= m_thrustFactor;
			m_velocity -= m_brakeForce;
		}

		public void Move ()
		{
			m_altitude = CalculateAltitude ();
			m_acceleration = CalculateAcceleration ();
			m_velocity = CalculateVelocity ();
			m_rigidbody.velocity = transform.forward * m_velocity;
		}

		public void Roll (float inputAxis)
		{
			transform.RotateAround (transform.position, transform.forward, inputAxis * m_rollFactor);
		}

		public void Pitch (float inputAxis)
		{
			transform.RotateAround (transform.position, transform.right, inputAxis * m_pitchFactor);
		}

		public void Yaw (float inputAxis)
		{
			transform.RotateAround (transform.position, transform.up, inputAxis * m_yawFactor);
		}

		void Stall ()
		{
			if (m_velocity < m_stallVelocity)
			{
				// stall code goes here
			}
		}

		float CalculateVelocity ()
		{
			return Mathf.Clamp (m_velocity + m_acceleration * Time.deltaTime, 0f, m_maxVelocity);
		}

		float CalculateAcceleration ()
		{
			return Mathf.Clamp (m_maxAcceleration * m_thrust / m_maxThrust, 0f, m_maxAcceleration);
		}

		private float CalculateAltitude ()
		{
			// Altitude calculations - we raycast downwards from the aeroplane
			// starting a safe distance below the plane to avoid colliding with any of the plane's own colliders
			var ray = new Ray (transform.position - Vector3.up * 3, -Vector3.up);
			RaycastHit hit;
			return Physics.Raycast (ray, out hit) ? hit.distance + 3 : transform.position.y;
		}
	}
}