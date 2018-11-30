using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private const string ZOMBIE_TAG = "Zombie";

    [SerializeField] private int health = 3;
    
	void Awake () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK, this.TakeDamage);
	}

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ZOMBIE_TAG)
        {
            Zombie zombieComp = other.gameObject.GetComponent<Zombie>();
            zombieComp.BeginAttack();
        }
    }

    void TakeDamage()
    {
        Debug.Log("Player health: " + health);
        health -= 1;
        if(health <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
    }
}
