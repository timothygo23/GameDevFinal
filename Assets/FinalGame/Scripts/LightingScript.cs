using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour {

    [SerializeField] private GameObject lights;
    [SerializeField] private Light flashlight;
    [SerializeField] private int time;
    [SerializeField] private int flashlightTriggerTime;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float flashlightOffTime;
    [SerializeField] private float flashlightOnTime;

    private float dimRate;
    private float currentTime = 0;

    private Coroutine co;

    public void Start()
    {
        dimRate = (float)(1 / (float)time);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        lightsDim(dimRate);

        if (currentTime >= flashlightTriggerTime && currentTime < flashlightOffTime)
        {
            co = StartCoroutine(Flicker());
        } else if (currentTime >= flashlightOffTime && currentTime < flashlightOnTime)
        {
            StopAllCoroutines();
            flashlight.enabled = false;
        } else if (currentTime >= flashlightOnTime)
        {
            flashlight.enabled = true;
        }
    }

    private void lightsDim(float value)
    {

        foreach (Transform child in transform)
        {
            var intensity = child.gameObject.GetComponent<Light>().intensity;
            child.gameObject.GetComponent<Light>().intensity = Mathf.Clamp(intensity - value * Time.deltaTime, 0, 1);
        }
            
    }

    IEnumerator Flicker()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            flashlight.enabled = !flashlight.enabled;
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

}
