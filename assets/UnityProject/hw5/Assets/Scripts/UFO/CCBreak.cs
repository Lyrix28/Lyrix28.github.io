using UnityEngine;
using System.Collections;

public class CCBreak: SSAction
{
	public FirstController sceneController; 

	public static CCBreak GetSSAction()
	{  
		return ScriptableObject.CreateInstance<CCBreak>();
	}  
	// Use this for initialization  
	public override void Start()
	{
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;
	}

	public override void Update() {

		switch (sceneController.round) {
		case 1:
		case 2:
			sceneController.score += 1;
			break;
		case 3:
		case 4:
			sceneController.score += 2;
			break;
		case 5:
		case 6:
			sceneController.score += 4;
			break;
		}

		if (sceneController.round % 2 == 0) {
			if (sceneController.flag) {
				sceneController.onFlying = false;
				sceneController.flag = false;
			} else
				sceneController.flag = true;
		} else {
			sceneController.onFlying = false;
		}
		DiskFactory.instance.freeDisk (this.gameobject);
		this.destroy = true;  
		this.callback.SSActionEvent(this);
	}
}