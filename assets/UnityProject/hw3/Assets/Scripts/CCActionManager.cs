using UnityEngine;
using System.Collections;

public class CCActionManager : SSActionManager, ISSActionCallback
{
	public FirstController sceneController;
	public CCGetOnShip getOn;
	public CCGetOffShip getOff;
	public CCShipMove shipMove;

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
		if (Input.GetMouseButtonDown(0))
			flag = true;
		else if (Input.GetMouseButtonUp(0) && flag && sceneController.Check() == 0) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			if(Physics.Raycast(ray,out hitInfo))
			{
				GameObject gameObj = hitInfo.collider.gameObject;
				if (gameObj.name.Equals ("Ship(Clone)")) {
					shipMove = CCShipMove.GetSSAction();  
					this.RunAction(gameObj, shipMove, this);  
				} else if (gameObj.name.Equals ("Good(Clone)") || gameObj.name.Equals ("Bad(Clone)")) {
					if (gameObj.transform.position.x > 1.5 || gameObj.transform.position.x < -1.5) {
						getOn = CCGetOnShip.GetSSAction();
						this.RunAction(gameObj, getOn, this);
					} else {
						getOff = CCGetOffShip.GetSSAction();
						this.RunAction(gameObj, getOff, this);
					}
				} else
					return;
			}  
		}

		base.Update();  
	}
}
