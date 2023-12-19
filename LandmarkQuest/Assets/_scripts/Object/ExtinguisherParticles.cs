using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherParticles : MonoBehaviour
{
   void Start()
    {
        AudioManager.instance.Playing("Extinguish");
    }
    public float extinguisherDamage = 0.1f;

    //Check for collision
    void OnParticleCollision(GameObject other)
    {
            Vulnerable obj = other.GetComponent<Vulnerable>();
        if (obj != null)
        {
            obj.TakeDamage(extinguisherDamage);
        }
    }
}
