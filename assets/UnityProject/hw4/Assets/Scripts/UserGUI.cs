using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

	private IUserAction action;
	private bool onRunning = false;

	void Start () {
		action = SSDirector.Instance.currentSceneController as IUserAction;
	}

	void OnGUI() {
		if (GUI.Button (new Rect (0, 0, 100, 50), onRunning?"Stop":"Start")) {
			if (onRunning) {
				onRunning = false;
				action.Stop ();
			} else {
				onRunning = true;
				action.Continue ();
			}
		}
		if (GUI.Button (new Rect (0, 50, 100, 50), "Restart")) {
			onRunning = true;
			action.Restart ();
		}

	}

}