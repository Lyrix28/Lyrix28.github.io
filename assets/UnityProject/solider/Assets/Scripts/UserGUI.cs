using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

	private IUserAction action;

	void Start () {
		action = SSDirector.Instance.currentSceneController as IUserAction;
	}

	void OnGUI() {
		

		/*if (GUI.Button (new Rect (0, 0, 100, 50), "Restart")) {
			action.Restart ();
		}*/

	}

}