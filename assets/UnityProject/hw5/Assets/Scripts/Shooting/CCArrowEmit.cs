using UnityEngine;
using System.Collections;

public class CCArrowEmit: SSAction
{
	public ShootingController sceneController; 

	public static CCArrowEmit GetSSAction()
	{  
		return ScriptableObject.CreateInstance<CCArrowEmit>();
	}  
	// Use this for initialization  
	public override void Start()
	{
		sceneController = (ShootingController)SSDirector.Instance.currentSceneController;
		var d = Camera.main.ScreenPointToRay (Input.mousePosition).direction;
		gameobject.SetActive (true);
		sceneController.head.SetActive (true);
		transform.position = new Vector3(d.x,d.y+1,0);
		transform.rotation = Quaternion.Euler(90,0,0);
		var rig = gameobject.GetComponent<Rigidbody>() as Rigidbody;
		rig.isKinematic = false;
		rig.velocity = Vector3.zero;
		rig.AddForce (d*30, ForceMode.Impulse);
		rig.AddForce (sceneController.wind, ForceMode.Impulse);


	}

	public override void Update() {
		if (sceneController.head.activeSelf) {
			
			if (transform.position.z > 8) {
				transform.position = Vector3.up;
				sceneController.head.SetActive (false);
				gameobject.SetActive (false);
				this.callback.SSActionEvent (this);
			}
		} else {
			var rig = gameobject.GetComponent<Rigidbody>() as Rigidbody;
			rig.isKinematic = true;
			this.destroy = true;
			this.callback.SSActionEvent (this,objectParam:gameobject);
		}

	}
}
