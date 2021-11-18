using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    Animator animator;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(isProvoked)
        {
            EngageTarget();
        } else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    public void OnDamageTaken()
        //Function being used in BroadcastMessage: (EnemyHealth, 17)
    {
        isProvoked = true;
    }

    void EngageTarget()
    {
        FaceTarget();

        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();

        }
    }

    void ChaseTarget()
    {
        animator.SetBool("Attack", false);
        animator.SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        animator.SetBool("Attack", true);
    }

    void FaceTarget()
    {
        //Interested in direction but don't want to apply any of the magnitude or distance
        Vector3 direction = (target.position - transform.position).normalized;

        //Derives from the direction set above
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //Slerp = Spherical Interpolation: allows us to rotate smoothly between 2 Vectors
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
