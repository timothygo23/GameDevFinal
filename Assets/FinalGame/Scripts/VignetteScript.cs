using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class VignetteScript : MonoBehaviour {

    [SerializeField] PostProcessingProfile ppp;

    

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        var vignette = ppp.vignette.settings;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vignette.intensity += 0.14f;
            ppp.vignette.settings = vignette;
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            vignette.intensity -= 0.14f;
            ppp.vignette.settings = vignette;
        }
	}
}
