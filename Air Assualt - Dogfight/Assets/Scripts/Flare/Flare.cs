using UnityEngine;
using System.Collections;

public class Flare : MonoBehaviour
{
    public float minLife = 75f;
    public float maxLife = 100f;
    public float life;
    public float decayFactor = .1f;

    void Awake()
    {
        life = Random.Range(minLife, maxLife);
    }

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    void FixedUpdate()
    {
        Decay();
    }



    void Decay()
    {
        life -= decayFactor;

        if (life <= 0f)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }
}
