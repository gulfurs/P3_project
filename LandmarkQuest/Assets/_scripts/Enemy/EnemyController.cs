using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f; //Player Detection range

    Transform target; //Reference to player
    NavMeshAgent agent; //Reference to AI
 
// Start is called before the first frame update
void Start()
    {
        target = PlayerManager.PlayerInstance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        //Move towards the target
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            //If within attacking range
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget(); //Make sure to face towards the target
            }

        }
    }

    //Rotate to face the target
    void FaceTarget()
    {
        //Find direction towards target
        Vector3 direction = (target.position - transform.position).normalized;
        //Faces direction while not changing y-rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        //Smooth out rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //Visualize radius gizmo (Only visible in Inspector) 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}