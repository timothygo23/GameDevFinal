using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour {
    


    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_GUN_SHOT_SHAKE, this.GunShotShake);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK_SHAKE, this.ZombieAttackShake);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_GUN_SHOT_SHAKE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK_SHAKE);
    }

    private void GunShotShake ()
    {
        StartCoroutine(Shake(0.1f, 0.1f));
    }

    private void ZombieAttackShake()
    {

    }

    IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elasped = 0.0f;

        while(elasped < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elasped += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
