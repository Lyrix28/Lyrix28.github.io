using UnityEngine;
using System.Collections;

public class CCActionManager : SSActionManager, ISSActionCallback
{
	public FirstController sceneController;

	private CCMAct mAct;
	private CCPAct pAct;

	public void SSActionEvent(SSAction source,
		SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0,
		string strParam = null,
		System.Object objectParam = null) {}

	void Start () {
		sceneController = SSDirector.Instance.currentSceneController as FirstController;
		sceneController.actionManager = this;  
	}  

	protected new void Update () {
		if (sceneController.over)
			return; 

		pAct = CCPAct.GetSSAction();
		this.RunAction (sceneController.player, pAct, this);

		foreach (var gb in sceneController.mons) {
			int ran = Random.Range (0, 10);
			var data = gb.GetComponent<MonsterData> ();
			if (data.speed == 2f) {
				if ((gb.transform.position - sceneController.player.transform.position).sqrMagnitude > 50f) {
					GameEventManager.Instance.OnLostGoal ();
					gb.GetComponent<MonsterData> ().speed = 1f;
				} else {
					gb.GetComponent<MonsterData> ().time = 0f;
					gb.transform.LookAt (sceneController.player.transform.position);
				}

			} else if (ran == 1 && data.time > 10f) {
				gb.GetComponent<MonsterData> ().time = 0f;
				gb.GetComponent<MonsterData> ().direction = new Vector3 (Random.Range (-30,30), 0, Random.Range(-30,30));
				gb.transform.LookAt (gb.GetComponent<MonsterData> ().direction);
			}
				
			mAct = CCMAct.GetSSAction();
			this.RunAction (gb, mAct, this);
		}

		base.Update();  
	}
}
