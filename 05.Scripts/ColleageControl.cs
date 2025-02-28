using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum ColleageState{
    IDLE,
    PLAYERTRACE,
    ENEMYTRACE,
    ATTACK,
    DAMAGE,
    DIE
}
public class ColleageControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject player;
    public GameObject target;

    public ColleageState state;
    public float Movespeed = 6f;
    public float stopDistance = 4f;
    public float enemyTraceDistance = 6f;
    public float playerTraceDistance = 10f;


    private readonly int hashIsMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        target = GameObject.FindWithTag("Enemy");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.destination = player.transform.position;
        agent.stoppingDistance = stopDistance;
        agent.speed = Movespeed;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= playerTraceDistance) //플레이어와의 거리가 playerTraceDistance 이내이고,
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= stopDistance) //enemy와의 거리가 stopDistance 이내면 enemy 공격 시작
            {
                state = ColleageState.ATTACK;
            }
            else if (Vector3.Distance(transform.position, target.transform.position) <= enemyTraceDistance) //enemy와의 거리가 enemyTraceDistance 이내면 enemy 추적
            {
                state = ColleageState.ENEMYTRACE;
            }
            else //enemy가 멀리 있고, player와의 거리가 stopDistance 이내면 idle 상태
            {
                if(Vector3.Distance(transform.position, player.transform.position) >= stopDistance)
                {
                    state = ColleageState.PLAYERTRACE;
                }
                else state = ColleageState.IDLE;
            }
               
        }
        else //플레이어와의 거리가 playerTraceDistance를 초과하는 경우
        {
            //플레이어 따라감.
            state = ColleageState.PLAYERTRACE;
        }

        switch (state)
        {
            case ColleageState.IDLE:
                agent.destination = player.transform.position;
                animator.ResetTrigger(hashAttack);
                animator.SetBool(hashIsMove, false);
                agent.isStopped = true;
                break;
           
            case ColleageState.PLAYERTRACE:
                agent.destination = player.transform.position;
                animator.SetBool(hashIsMove, true);
                agent.isStopped = false;
                break;

            case ColleageState.ENEMYTRACE:
                agent.destination = target.transform.position;
                animator.SetBool(hashIsMove, true);
                agent.isStopped = false;
                break;

            case ColleageState.ATTACK:
                agent.destination = target.transform.position;
                animator.SetBool(hashIsMove, false);
                animator.SetTrigger(hashAttack);
                agent.isStopped = true;
                break;
        }
        
        
        
    }
}
