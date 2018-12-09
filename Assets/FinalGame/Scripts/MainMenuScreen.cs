using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : View {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnStartClicked()
    {
        this.Hide();
        View view = ViewHandler.Instance.Show(ViewNames.LOADING_SCREEN);
    }
    
}
