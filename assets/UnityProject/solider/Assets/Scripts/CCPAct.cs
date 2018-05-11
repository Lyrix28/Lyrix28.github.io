using UnityEngine;
using System.Collections;

public class CCPAct: SSAction
{
	public FirstController sceneController; 

	private Animator ani;

	public static CCPAct GetSSAction()
	{  
		return ScriptableObject.CreateInstance<CCPAct>();
	}  
	// Use this for initialization  
	public override void Start()
	{
		ani = gameobject.GetComponent<Animator> ();
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;

		if (transform.rotation.z > 0.000001f || transform.rotation.x > 0.000001f) {
			var r = transform.rotation;
			Quaternion q = new Quaternion ();
			q.Set(0f,r.y,0f,r.w);
			transform.rotation = q;
		}

		if (transform.position.y < 1f)
			gameobject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
	}

	public override void Update() {
		
		if (Input.GetKey (KeyCode.W)) {
			ani.SetTrigger ("run");
			transform.Translate (Vector3.forward*Time.deltaTime*5);
		}
		if (Input.GetKey (KeyCode.S)) {
			
			transform.Rotate (0,180*Time.deltaTime,0);
		}
		if (Input.GetKey (KeyCode.A)) {
			
			transform.Rotate (0,-90*Time.deltaTime,0);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (0,90*Time.deltaTime,0);
		}

		this.destroy = true;
		this.callback.SSActionEvent (this);
	}
}


