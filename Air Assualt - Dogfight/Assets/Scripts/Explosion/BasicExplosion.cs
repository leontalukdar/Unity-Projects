using UnityEngine;
using System.Collections;

public class BasicExplosion : MonoBehaviour
{
    public float explosionForce = 30000f;
    public float explosionRadius = 100f;
    public float lifeTime = 3f;

    // Use this for initialization
    void Start()
    {
        //lifeTime = GetComponent<ParticleSystem>().duration;

        int layerMask = 1 << 8;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
