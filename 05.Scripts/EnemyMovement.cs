using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    IDLE,
    TRACE,
    ATTACK,
    DAMAGE,
    DIE
}

public class EnemyMovement : MonoBehaviour
{
    public EnemyState state = EnemyState.IDLE;
    public NavMeshAgent agent;
    public Transform target;
    public float traceDistance = 30f;
    public float attackDistance = 3f;
    public Vector3 originPos;

    private int hashIsMove = Animator.StringToHash("IsMove");
    private int hashPunch = Animator.StringToHash("Punch");

    public Animator[] animator;

    void Start()
    {
        originPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            state = EnemyState.ATTACK;
        }
        else if (Vector3.Distance(transform.position, target.position) <= traceDistance)
        {
            state = EnemyState.TRACE;
        }
        else
        {
            state = EnemyState.IDLE;
        }

        switch (state)
        {
            case EnemyState.IDLE:
                agent.destination = originPos;
                foreach (Animator anim in animator)
                {
                    if (agent.remainingDistance < 3f)
                    {
                        anim.SetBool(hashIsMove, false);
                        agent.isStopped = true;
                    }
                    //원래 자리로 돌아갈 때 걷는 애니메이션 실행되도록 함.
                    else
                    {
                        anim.SetBool(hashIsMove, true);
                        agent.isStopped = false;
                    }
                }
                break;
            case EnemyState.TRACE:
                agent.isStopped = false;
                agent.destination = target.position;
                foreach (Animator anim in animator) anim.SetBool(hashIsMove, true);
                break;
            case EnemyState.ATTACK:
                agent.isStopped = false;
                agent.destination = target.position;
                transform.LookAt(target.position);
                foreach (Animator anim in animator)
                {
                    anim.SetBool(hashIsMove, false);
                    anim.SetTrigger(hashPunch);
                }
                break;
            default:
                break;
        }
    }
}
