using UnityEngine;
using System.Collections;

public class CCPhysicEmit: SSAction
{
	public FirstController sceneController; 

	public static CCPhysicEmit GetSSAction()
	{  
		return ScriptableObject.CreateInstance<CCPhysicEmit>();
	}  
	// Use this for initialization  
	public override void Start()
	{
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;
		int round = sceneController.round;
		if (round % 2 == 0) {
			round /= 2;
		}
		var rig = gameobject.AddComponent(typeof(Rigidbody)) as Rigidbody;
		rig.velocity = Vector3.zero;
		rig.AddForce (new Vector3 (Random.Range(-10f,10f),round,round*3), ForceMode.Impulse);
	}

	public override void Update() {
		if (!gameobject.activeSelf) {
			this.destroy = true;
			this.callback.SSActionEvent (this);
		} else {
			if (this.transform.position.y < -10)  
			{  
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
				this.enable = false;  
				this.callback.SSActionEvent(this);  
			} 
		}
	}
}

