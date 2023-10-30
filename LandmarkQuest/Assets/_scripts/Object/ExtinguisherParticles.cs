using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherParticles : MonoBehaviour
{
    //Damage
    public float extinguisherDamage = 0.1f;

    //Check for collision
    void OnParticleCollision(GameObject other)
    {
        FireHealth fire = other.GetComponent<FireHealth>();
        if (fire != null)
        {
            fire.TakeDamage(extinguisherDamage);
        }
    }
}
