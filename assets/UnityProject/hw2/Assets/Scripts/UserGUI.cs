using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

	private IUserAction action;
	private bool flag = false;
	private int state = 0;

	void Start () {
		action = SSDirector.Instance.currentSceneController as IUserAction;
	}

	void OnGUI() {
		if (GUI.Button (new Rect (300, 50, 150, 50), "RESTART")) {
			action.Restart ();
		}
		if (state != 0) {
			if (state == 1)
				GUI.Label (new Rect (400, 105, 50, 50), "Win.");
			else
				GUI.Label (new Rect (400, 105, 50, 50), "Lose.");
			return;
		}
		if (Input.GetMouseButtonDown(0))
			flag = true;
		else if (Input.GetMouseButtonUp(0) && flag) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			if(Physics.Raycast(ray,out hitInfo))
			{
				GameObject gameObj = hitInfo.collider.gameObject;
				Debug.Log ("jump");
				action.Clicked (gameObj);
				state = action.Check ();
				if (state != 0) {
					action.GameOver ();
				}
				Debug.Log ("back");
			}  
		}
	}
}