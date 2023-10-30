using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHealth : MonoBehaviour
{
    public float maxHP = 100;
    [SerializeField]
    private float currentHP;
    public ParticleSystem fireParticles;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        // Adjusts StartLifetime based on the current HP
        var main = fireParticles.main;
        main.startLifetime = currentHP / maxHP * 10f; 

        // If fire is out
        if (currentHP <= 0)
        {
            
        }
    }

    public void TakeDamage(float damageAmount)
    {
        //Take damage
        currentHP -= damageAmount;

        // Ensure HP doesn't go below 0
        currentHP = Mathf.Max(currentHP, 0);
    }
}
