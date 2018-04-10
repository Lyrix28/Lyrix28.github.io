using UnityEngine;
using System.Collections;

public class CCShipMove : SSAction
{
	public FirstController sceneController;  

	public static CCShipMove GetSSAction()  
	{  
		CCShipMove action = ScriptableObject.CreateInstance<CCShipMove>();  
		return action;  
	}  
	// Use this for initialization  
	public override void Start()  
	{  
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;  
	} 

	public override void Update ()
	{
		if (sceneController.left == null && sceneController.right == null)
			return;
		else {
			Vector3 p;
			if (sceneController.side == 1) {
				p = Vector3.left * 2;
				sceneController.side = -1;
			} else {
				p = Vector3.right * 2;
				sceneController.side = 1;
			}
			sceneController.ship.transform.position += p;
			if (sceneController.left != null)
				sceneController.left.transform.position += p;
			if (sceneController.right != null)
				sceneController.right.transform.position += p;
		}

		this.destroy = true;  
		this.callback.SSActionEvent(this);
	}
}