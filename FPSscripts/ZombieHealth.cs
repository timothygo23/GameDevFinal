using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float Health = 5;
    private Animator _animator;
    public bool isCritter = false;
    void Start()
    {
        _animator = GetComponent<Animator>();

        if (Health == 1)
            isCritter = true;

    }

    public void TakeDamage(float damage)
    {
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
            Destroy(gameObject);
        }
        else
        {
            _animator.SetTrigger("Death");
        }
        
     
    }

}
