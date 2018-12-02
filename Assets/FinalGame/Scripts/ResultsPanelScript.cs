using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsPanelScript : View {

    [SerializeField] private Text timeText;
    [SerializeField] private Text killsText;
    private float time;
    private int kills;

	// Use this for initialization
	void Start () {
		
	}

    public void SetData(float time, int kills)
    {
        this.time = time;
        this.kills = kills;

        //update ui
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        timeText.text = "Time: " + string.Format("{0}:{1}", minutes, seconds);
        killsText.text = "Kills: " + kills.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRestart()
    {
        LoadManager.Instance.LoadScene("FinalGameScene");
    }
}
