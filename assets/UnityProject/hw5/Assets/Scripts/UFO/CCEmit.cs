using UnityEngine;
using System.Collections;

public class CCEmit: SSAction
{
	public FirstController sceneController; 

	private float speed;
	private Vector3 direction;
	float time = 0;

	public static CCEmit GetSSAction()
	{  
		return ScriptableObject.CreateInstance<CCEmit>();
	}  
	// Use this for initialization  
	public override void Start()
	{
		sceneController = (FirstController)SSDirector.Instance.currentSceneController;
		var color = this.gameobject.GetComponent<Renderer> ().material.color;
		if (color == Color.white) {
			speed = 4.0f;
		} else if (color == Color.blue) {
			speed = 6.0f;
		} else if (color == Color.black) {
			speed = 8.0f;
		}
		direction = new Vector3 (0, 1, 1);
	}

	public override void Update() {
		if (!gameobject.activeSelf) {
			this.destroy = true;
			this.callback.SSActionEvent (this);
		} else {
			time += Time.deltaTime;
			transform.position += Vector3.down * 9.8f * time * Time.deltaTime;
			transform.position += direction * speed * Time.deltaTime;
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
