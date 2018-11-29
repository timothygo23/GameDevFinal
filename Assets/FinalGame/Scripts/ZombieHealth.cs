using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float Health = 5;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
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
        _animator.SetTrigger("Death");
    }

}
