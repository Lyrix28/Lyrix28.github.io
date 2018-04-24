using UnityEngine;
using System.Collections;

public class CCArrowTremble: SSAction
{
	public ShootingController sceneController; 

	private float leftTime = 0.1f;

	private Vector3 trueLocation;

	public static CCArrowTremble GetSSAction()
	{  
		return ScriptableObject.CreateInstance<CCArrowTremble>();
	}  
	// Use this for initialization  
	public override void Start()
	{
		sceneController = (ShootingController)SSDirector.Instance.currentSceneController;
		trueLocation = transform.position;

	}

	public override void Update() {
		leftTime -= Time.deltaTime;
		if (leftTime < 0) {
			transform.position = trueLocation;
			this.destroy = true;
			this.callback.SSActionEvent (this);
		} else {
			transform.position += Vector3.left * Random.Range (-0.1f,0.1f);

		}

	}
}

