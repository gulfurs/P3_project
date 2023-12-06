using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    Transform target;
    //References component
    NavMeshAgent agent;

    public bool atDestination = false;

    // Start is called before the first frame update
    void Start()
    {
        //Gets component
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null && !atDestination) 
        {
            agent.SetDestination(target.position);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                atDestination = true;
            }
            FaceTarget();
        }
    }

    //Moves to point
    public void MoveToPoint(Vector3 point)
    {
        atDestination = false;
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 1.5f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    public void StopFollowTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

    void FaceTarget()
    {
        //Find direction towards target
        Vector3 direction = (target.position - transform.position).normalized;
        //Faces direction while not changing y-rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        //Smooth out rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}