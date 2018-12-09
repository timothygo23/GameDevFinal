using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelScript : View {

    [SerializeField] private Text time;
    [SerializeField] private Text kills;

    private int killsValue = 0;
    private float currentTime = 0;

    private bool isAlive = true;

	void Awake () {
        // zombie kill listener
        this.rectTransform = this.GetComponent<RectTransform>();
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_ZOMBIE_DIE, this.updateKills);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_PLAYER_DIE, this.GameOver);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_ZOMBIE_ATTACK);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_PLAYER_DIE);
    }

    private void Start()
    {
        time.text = string.Format("{0:N0}", 0);
        kills.text = killsValue.ToString();
    }

    void Update () {
        if(isAlive)
        {
            currentTime += Time.deltaTime;

            string minutes = Mathf.Floor(currentTime / 60).ToString("00");
            string seconds = (currentTime % 60).ToString("00");

            time.text = string.Format("{0}:{1}", minutes, seconds);
        }
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

    public void GameOver()
    {
        isAlive = false;

        View resultView = ViewHandler.Instance.Show(ViewNames.RESULT_SCREEN);
        ResultsPanelScript rps = resultView.GetComponent<ResultsPanelScript>();
        rps.SetData(currentTime, killsValue);

        this.gameObject.SetActive(false);
    }
}
