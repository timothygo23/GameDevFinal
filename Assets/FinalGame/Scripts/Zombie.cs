using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    private float speed;

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
        this.speed = speed;
        agent.speed = speed;
    }

}
