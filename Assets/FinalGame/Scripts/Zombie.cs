using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{

    private const string ATTACK_KEY = "Attack";

    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    private float speedWalk;
    private bool isAttacking = false;
    [SerializeField] private float attackInterval = 2;

    // Use this for initialization
    void Start()
    {
        agent.SetDestination(player.position);
    }

    public void SetTarget(Transform target)
    {
        this.player = target;
    }

    public void SetSpeed(float speed)
    {
        this.speedWalk = speed;
        agent.speed = speed;
    }

    public void BeginAttack()
    {
        if (!isAttacking)
        {
            InvokeRepeating("Attack", 0, attackInterval);
            isAttacking = true;
        }
    }

    public void EndAttack()
    {
        CancelInvoke();
    }

    private void Attack()
    {
        animator.SetTrigger(ATTACK_KEY);
        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK);
    }

}
