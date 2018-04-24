using UnityEngine;
using System.Collections;

public class CCShootingActionManager : SSActionManager, ISSActionCallback
{
	public ShootingController sceneController;

	private CCArrowEmit arrowEmit;

	private bool flag = false;

	public void SSActionEvent(SSAction source,
		SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0,
		string strParam = null,
		System.Object objectParam = null) {}

	void Start () {
		sceneController = SSDirector.Instance.currentSceneController as ShootingController;
		sceneController.actionManager = this;  
	}  

	protected new void Update () {



		if (Input.GetMouseButtonDown(0))
			flag = true;
		else if (Input.GetMouseButtonUp(0) && flag) {
			/*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			if(Physics.Raycast(ray,out hitInfo))
			{
				GameObject gameObj = hitInfo.collider.gameObject;
				arrowEmit = CCArrowEmit.GetSSAction();  
				this.RunAction(gameObj, arrowEmit, this);  
			}*/
			sceneController.count++;
			arrowEmit = CCArrowEmit.GetSSAction();
			this.RunAction (sceneController.arrow, arrowEmit, this);
		}

		base.Update();  
	}
}
