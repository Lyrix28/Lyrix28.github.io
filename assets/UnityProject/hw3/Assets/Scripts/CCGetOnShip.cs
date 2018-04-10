using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCGetOnShip : SSAction
{
	public FirstController sceneController;  

	public static CCGetOnShip GetSSAction()  
	{  
		CCGetOnShip action = ScriptableObject.CreateInstance<CCGetOnShip>();  
		return action;  
	}  
	// Use this for initialization  
	public override void Start()  
	{  
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;  
	}  

	// Update is called once per frame  
	public override void Update()  
	{
		if (sceneController.left != null && sceneController.right != null)
			goto l1;
		if (sceneController.side == -1) {
			if (gameobject.transform.position.x > 0)
				goto l1;
			sceneController.left_shore.Push (gameobject.transform.position);
			if (gameobject.name.Equals ("Good(Clone)")) {
				sceneController.good_on_left--;
			} else {
				sceneController.bad_on_left--;
			}
		} else {
			if (gameobject.transform.position.x < 0)
				goto l1;
			sceneController.right_shore.Push (gameobject.transform.position);
		}
		Vector3 p = Vector3.zero;
		if (sceneController.left == null) {
			p = Vector3.left;
			sceneController.left = gameobject;
		} else if (sceneController.right == null) {
			p = Vector3.right;
			sceneController.right = gameobject;
		}
		gameobject.transform.position = sceneController.ship.transform.position + Vector3.up + p*0.5f;

		l1:
		this.destroy = true;
		this.callback.SSActionEvent(this);
	}
}