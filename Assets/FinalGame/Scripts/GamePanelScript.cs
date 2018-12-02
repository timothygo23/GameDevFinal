using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelScript : MonoBehaviour {

    [SerializeField] private Text time;
    [SerializeField] private Text kills;

    private int killsValue = 0;
    private float currentTime = 0;

	void Awake () {
        // zombie kill listener
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_ZOMBIE_DIE, this.updateKills);

        time.text = string.Format("{0:N0}", 0);
        kills.text = killsValue.ToString();
    }
	
	void Update () {
        currentTime += Time.deltaTime;

        string minutes = Mathf.Floor(currentTime / 60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");

        time.text = string.Format("{0}:{1}", minutes, seconds);
	}

    private void updateKills ()
    {
        killsValue++;
        kills.text = killsValue.ToString();
    }

    public float getCurrentTime()
    {
        return this.currentTime;
    }

    public int getKills()
    {
        return this.killsValue;
    }
}
