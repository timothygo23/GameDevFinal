using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent.SetDestination(player.position);
	}

    private void Update()
    {
        //transform.LookAt(player);
    }
}
