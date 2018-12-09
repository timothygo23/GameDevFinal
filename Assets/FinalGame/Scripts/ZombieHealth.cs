using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public float Health = 5;
    public bool isCritter = false;

    private ParticleSystem _particle;

    void Start()
    {
        if (Health == 1)
        {
            isCritter = true;
            _particle = GetComponentInChildren<ParticleSystem>();
        }
   
    }

    public void TakeDamage(float damage)
    {
       //_particle.Play();
        if (Health <= 0)
        {
            return;
        }

        Health -= damage;

        if (Health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        BoxCollider[] boxColliders = gameObject.GetComponents<BoxCollider>();
        foreach (BoxCollider bc in boxColliders) bc.enabled = false;

        MeshCollider[] meshColliders = gameObject.GetComponents<MeshCollider>();
        foreach (MeshCollider mc in meshColliders) mc.enabled = false;

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.mute = !audioSource.mute;

        if (isCritter)
        {
            _particle.Play();
            GameObject.Destroy(transform.GetChild(0).GetChild(1).gameObject);
            GameObject.Destroy(transform.GetChild(0).GetChild(2).gameObject);
            //Destroy(gameObject);
        }
        else
        {

            _animator.SetTrigger("Death");
        }

        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false; //this stops it from moving

        Zombie zombieComp = GetComponent<Zombie>();
        zombieComp.EndAttack();

        // to know when Zombie dies
        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_ZOMBIE_DIE);

        Destroy(this.gameObject, 5f);
    }
}
