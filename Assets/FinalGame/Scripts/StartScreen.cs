using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : View {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnPlayClicked()
    {
        Debug.Log("Play Clicked!");
        this.Hide();
        ViewHandler.Instance.Show(ViewNames.MAIN_MENU_SCREEN);
        
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit Clicked!");
        TwoChoiceDialog dialog = (TwoChoiceDialog)DialogBuilder.Create(DialogBuilder.DialogType.CHOICE_DIALOG);
        dialog.SetMessage("Are you sure you want to quit the game?");
        dialog.SetConfirmText("YES");
        dialog.SetCancelText("No");
        dialog.SetOnConfirmListener(() =>
        {
            Debug.Log("About to quit");
            Application.Quit();
        });
    }
}
