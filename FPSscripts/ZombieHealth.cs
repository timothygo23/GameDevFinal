using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float Health = 5;
    private Animator _animator;
    public bool isCritter = false;
    private ParticleSystem _particle;

    void Start()
    {
        _animator = GetComponent<Animator>();

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

        if (isCritter)
        {
            _particle.Play();
            GameObject.Destroy(transform.GetChild(1).gameObject);
            GameObject.Destroy(transform.GetChild(2).gameObject);
            //Destroy(gameObject);
        }
        else
        {
            _animator.SetTrigger("Death");
        }
        
     
    }

}
