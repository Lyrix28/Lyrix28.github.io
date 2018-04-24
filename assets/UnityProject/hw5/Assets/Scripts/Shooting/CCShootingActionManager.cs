using UnityEngine;
using System.Collections;

public class CCShootingActionManager : SSActionManager, ISSActionCallback
{
	public ShootingController sceneController;

	private CCArrowEmit arrowEmit;
	private CCArrowTremble arrowTremble;

	private bool flag = false;

	public void SSActionEvent(SSAction source,
		SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0,
		string strParam = null,
		System.Object objectParam = null) {
		if(objectParam != null)
		{
			arrowTremble = CCArrowTremble.GetSSAction();
			this.RunAction(objectParam as GameObject, arrowTremble, this);
		}
	}

	void Start () {
		sceneController = SSDirector.Instance.currentSceneController as ShootingController;
		sceneController.actionManager = this;  
	}  

	protected new void Update () {

		if (Input.GetMouseButtonDown(0))
			flag = true;
		else if (Input.GetMouseButtonUp(0) && flag) {
			sceneController.count++;
			arrowEmit = CCArrowEmit.GetSSAction();
			this.RunAction (sceneController.arrow, arrowEmit, this);
		}

		base.Update();  
	}
}
