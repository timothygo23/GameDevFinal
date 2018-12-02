using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    [SerializeField] private Animator animator;

	// Use this for initialization
	void Awake () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_CURSOR_SHOT, this.CursorShot);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_CURSOR_SHOT);
    }

    private void CursorShot()
    {
        animator.SetTrigger("Shot");
    }
}
