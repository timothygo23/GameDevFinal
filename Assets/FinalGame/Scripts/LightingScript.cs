using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour {

    [SerializeField] private GameObject lights;
    [SerializeField] private int time;

    private float dimRate;

    public void Start()
    {
        dimRate = (float)(1 / (float)time);
    }

    private void Update()
    {
        lightsDim(dimRate);
    }

    private void lightsDim(float value)
    {

        foreach (Transform child in transform)
        {
            var intensity = child.gameObject.GetComponent<Light>().intensity;
            child.gameObject.GetComponent<Light>().intensity = Mathf.Clamp(intensity - value * Time.deltaTime, 0, 1);
        }
            
    }

}
