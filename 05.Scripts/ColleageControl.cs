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
        if (Vector3.Distance(transform.position, player.transform.position) <= playerTraceDistance) //�÷��̾���� �Ÿ��� playerTraceDistance �̳��̰�,
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= stopDistance) //enemy���� �Ÿ��� stopDistance �̳��� enemy ���� ����
            {
                state = ColleageState.ATTACK;
            }
            else if (Vector3.Distance(transform.position, target.transform.position) <= enemyTraceDistance) //enemy���� �Ÿ��� enemyTraceDistance �̳��� enemy ����
            {
                state = ColleageState.ENEMYTRACE;
            }
            else //enemy�� �ָ� �ְ�, player���� �Ÿ��� stopDistance �̳��� idle ����
            {
                if(Vector3.Distance(transform.position, player.transform.position) >= stopDistance)
                {
                    state = ColleageState.PLAYERTRACE;
                }
                else state = ColleageState.IDLE;
            }
               
        }
        else //�÷��̾���� �Ÿ��� playerTraceDistance�� �ʰ��ϴ� ���
        {
            //�÷��̾� ����.
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
