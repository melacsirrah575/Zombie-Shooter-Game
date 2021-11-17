using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10f;

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

    void EngageTarget()
    {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
