using UnityEngine;
using System.Collections;

public class CCGetOffShip : SSAction
{
	public FirstController sceneController;  

	public static CCGetOffShip GetSSAction()
	{  
		CCGetOffShip action = ScriptableObject.CreateInstance<CCGetOffShip>();
		return action;
	}  
	// Use this for initialization  
	public override void Start()
	{
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;
	}

	public override void Update() {
		if (sceneController.side == 1)
			gameobject.transform.position = sceneController.right_shore.Pop();
		else {
			gameobject.transform.position = sceneController.left_shore.Pop();
			if (gameobject.name.Equals ("Good(Clone)"))
				sceneController.good_on_left++;
			else
				sceneController.bad_on_left++;
		}
		if (gameobject == sceneController.left)
			sceneController.left = null;
		else
			sceneController.right = null;

		this.destroy = true;  
		this.callback.SSActionEvent(this);
	}
}