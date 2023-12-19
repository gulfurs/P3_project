using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vulnerable : MonoBehaviour
{
    public float maxHP = 100;
    [SerializeField]
    private float currentHP;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Animator anim;
    private Hazardous hzrd;
    private EnemyController controller;
    private Collider boxCollider;
    private SphereCollider sphCollider;
    private AudioMaker audia;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hzrd = GetComponent<Hazardous>();
        controller = GetComponent<EnemyController>();
        boxCollider = GetComponent<Collider>();
        sphCollider = GetComponent<SphereCollider>();
        audia = GetComponent<AudioMaker>();
    }

    // Update is called once per frame
    void Update()
    {
        // If fire is out
        if (currentHP <= 0)
        {
            if (navMeshAgent != null)
                navMeshAgent.enabled = false;

            if (anim != null)
                anim.enabled = false;

            if (hzrd != null)
                hzrd.enabled = false;

            if (controller != null)
                controller.enabled = false;

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            if (boxCollider != null)
                boxCollider.enabled = false;

            if (sphCollider != null)
                sphCollider.radius = 1.0f;

            AudioManager.instance.Stop("Bee");

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
