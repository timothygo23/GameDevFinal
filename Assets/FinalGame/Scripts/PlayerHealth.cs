using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerHealth : MonoBehaviour {

    private const string ZOMBIE_TAG = "Zombie";

    [SerializeField] private int health = 3;
    [SerializeField] private PostProcessingProfile ppp;

    [SerializeField] private AudioSource damageAudio1;
    [SerializeField] private AudioSource damageAudio2;
    private bool playerDamageAudio1 = true;

    private float maxHealth;
    private float vignetteIncrement;

    void Awake () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK, this.TakeDamage);
        maxHealth = health;
        vignetteIncrement = (float)(1 / maxHealth);
        //vignetteIncrement = 0.01f;
    }

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK);

        var vignette = ppp.vignette.settings;
        vignette.intensity = 0.0f;
        ppp.vignette.settings = vignette;
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

        //audio
        if (playerDamageAudio1)
        {
            playerDamageAudio1 = false;
            damageAudio1.Play();
        }
        else
        {
            playerDamageAudio1 = true;
            damageAudio2.Play();
        }

        //red tint
        var vignette = ppp.vignette.settings;
        vignette.intensity += vignetteIncrement;
        ppp.vignette.settings = vignette;

        //camera shake
        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK_SHAKE);

        if (health <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
    }
}
