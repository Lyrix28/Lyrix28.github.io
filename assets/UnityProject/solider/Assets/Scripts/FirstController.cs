using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	public int score = 0;
	public bool over = false;
	public GameObject player;

	public List<GameObject> mons = new List<GameObject>();

	public SSActionManager actionManager { get; set; }

	void Awake() {
		SSDirector director = SSDirector.Instance;
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
	}

	public void LoadResources() {

		MonsterFactory mFactory = MonsterFactory.Instance;
		for (int i = 0; i < 10; i++) {
			GameObject mon = mFactory.GetMonster ();
			mon.transform.position = new Vector3 (Random.Range(-20, 20), 0, Random.Range(-20, 20));
			mons.Add (mon);
		}

		TombFactory tFactory = TombFactory.Instance;
		for (int i = 0; i < 5; i++) {
			GameObject tom = tFactory.GetTomb ();
			tom.transform.position = new Vector3 (Random.Range(-20, 20), 0, Random.Range(-20, 20));
		}
	}

	void OnGUI() {

		GUI.color = Color.green;
		GUI.Label (new Rect (750, 0, 60, 50), "Score: "+score.ToString());

		if (over) {
			GUI.color = Color.red;
			GUI.Label (new Rect (350, 100, 60, 50), "LOSE");
		}
	}

	void OnEnable() {
		GameEventManager.scoreEvent += Count;
		GameEventManager.gameOverEvent += GameOver;
	}

	void OnDesable() {
		GameEventManager.scoreEvent -= Count;
		GameEventManager.gameOverEvent -= GameOver;
	}

	public void Count () {
		score += 1;
	}

	public void GameOver() {
		over = true;

	}
}
