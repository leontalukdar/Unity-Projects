using UnityEngine;
using System.Collections;

public class Bullet
{
    public float speed { get ; set; }

    public float damage { get ; set; }

    public float effectiveRange { get ; set; }

    public Bullet(float damage, float speed, float range)
    {
        this.damage = damage;
        this.speed = speed;
        this.effectiveRange = range;
    }
}
