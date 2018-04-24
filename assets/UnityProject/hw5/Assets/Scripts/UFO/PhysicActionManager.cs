using UnityEngine;
using System.Collections;

public class PhysicActionManager : SSActionManager, ISSActionCallback
{
	public FirstController sceneController;
	public CCBreak diskBreak;
	public CCPhysicEmit diskEmit;

	private bool flag = false;

	public void SSActionEvent(SSAction source,
		SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0,
		string strParam = null,
		System.Object objectParam = null) {}

	void Start () {
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;
		sceneController.actionManager = this;  
	}  

	protected new void Update () {

		if (!sceneController.onRunning)
			return;
		if (!sceneController.onFlying) {
			if (!sceneController.TextMove ())
				return;
			if (sceneController.round % 2 == 0) {
				diskEmit = CCPhysicEmit.GetSSAction();
				this.RunAction(DiskFactory.instance.getDisk(sceneController.round), diskEmit, this);  
			}
			diskEmit = CCPhysicEmit.GetSSAction();
			this.RunAction(DiskFactory.instance.getDisk(sceneController.round), diskEmit, this);  
		}
		if (Input.GetMouseButtonDown(0))
			flag = true;
		else if (Input.GetMouseButtonUp(0) && flag) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			if(Physics.Raycast(ray,out hitInfo))
			{
				GameObject gameObj = hitInfo.collider.gameObject;
				diskBreak = CCBreak.GetSSAction();  
				this.RunAction(gameObj, diskBreak, this);  
			}
		}

		base.Update();  
	}
}

