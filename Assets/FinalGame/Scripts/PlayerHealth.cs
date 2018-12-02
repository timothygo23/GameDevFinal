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

    private float previousAperture;
    private float previousFocusDistance;

    private const string DEATH_ANIMATION_KEY = "Death";

    void Awake () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK, this.TakeDamage);
        maxHealth = health;
        vignetteIncrement = (float)(1 / maxHealth);
        //vignetteIncrement = 0.01f;

        var dop = ppp.depthOfField.settings;
        previousAperture = dop.aperture;
        previousFocusDistance = dop.focusDistance;
    }

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK);

        var vignette = ppp.vignette.settings;
        vignette.intensity = 0.0f;
        ppp.vignette.settings = vignette;

        var dop = ppp.depthOfField.settings;
        dop.aperture = previousAperture;
        dop.focusDistance = previousFocusDistance;
        ppp.depthOfField.settings = dop;
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
        //Debug.Log("Player health: " + health);
        if(health > 0)
        {
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
    }

    void GameOver()
    {
        Debug.Log("GameOver");

        var dop = ppp.depthOfField.settings;
        dop.aperture = 0;
        dop.focusDistance = 0.1f;
        ppp.depthOfField.settings = dop;

        CameraController cc = GetComponentInChildren<CameraController>();
        PlayerShootingController psc = GetComponentInChildren<PlayerShootingController>();
        cc.enabled = false;
        psc.enabled = false;

        Animator playerAnimator = GetComponent<Animator>();
        //playerAnimator.SetTrigger(DEATH_ANIMATION_KEY);
        playerAnimator.Play("PlayerDeath");

        Invoke("GameOverView", 1.5f);
    }

    public void GameOverView()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_PLAYER_DIE);
    }

    public bool isAlive()
    {
        if (health > 0)
            return true;
        else
            return false;
    }
}
