using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour
{
	public float speed = 5f;
	public float lifeTime = 10f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Life();
		Move();
	}

	void Life()
	{
		lifeTime -= Time.deltaTime;

		if(lifeTime <= 0f)
		{
			OnDeath();
		}
	}

	void Move()
	{
		transform.position += transform.forward * speed;
	}
	
	void OnDeath()
	{
		Destroy(gameObject);

	}

	void OnCollisionEnter()
	{
		OnDeath();
	}

	void Detonate()
	{

	}
}
