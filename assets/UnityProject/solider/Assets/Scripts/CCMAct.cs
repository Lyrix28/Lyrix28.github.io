using UnityEngine;
using System.Collections;

public class CCMAct: SSAction
{
	public FirstController sceneController; 

	private Animator ani;
	private MonsterData data;

	public static CCMAct GetSSAction()
	{  
		return ScriptableObject.CreateInstance<CCMAct>();
	}  
	// Use this for initialization  
	public override void Start()
	{
		ani = gameobject.GetComponent<Animator> ();
		data = gameobject.GetComponent<MonsterData> ();
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;

	}

	public override void Update() {
		
		data.time += Time.deltaTime;
		transform.Translate (Vector3.forward*Time.deltaTime*data.speed);
		if (data.speed == 2f) {
			ani.SetTrigger ("run");
		}
		this.destroy = true;
		this.callback.SSActionEvent (this);
	}
}

