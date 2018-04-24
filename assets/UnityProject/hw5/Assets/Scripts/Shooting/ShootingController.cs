using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ShootingController : MonoBehaviour, ISceneController, IUserAction {

	public int score = 0;

	public Vector3 wind;
	public GameObject arrow;
	public GameObject head;

	public bool flag = false;

	private bool flag2 = false;

	public  int count = 0;

	public SSActionManager actionManager { get; set; }

	void Awake() {
		SSDirector director = SSDirector.Instance;
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
	}

	public void LoadResources() {

		for (int i = 0; i < 5; i++) {
			var ring = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("Prefabs/ring"));
			ring.name = i.ToString ();
			if (i % 2 == 0) {
				ring.GetComponent<Renderer> ().material.color = Color.red;
			} else {
				ring.GetComponent<Renderer> ().material.color = Color.white;
			}
			ring.transform.position += new Vector3 (0,0,i*0.01f);
			ring.transform.localScale = new Vector3 (i+1,1,i+1);
		}

		arrow = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("Prefabs/arrow"));
		arrow.SetActive (false);
		head = arrow.GetComponentsInChildren<Transform> () [4].gameObject;

	}

	void OnGUI() {

		GUI.color = Color.green;
		GUI.Label (new Rect (750, 0, 60, 50), "Count: "+count.ToString());
		GUI.Label (new Rect (750, 50, 60, 50), "Score: "+score.ToString());
		GUI.Label (new Rect (400, 0, 200, 50), "Wind: "+wind.ToString());

	}

	void Update() {
		if (count % 3 == 0 && flag2) {
			flag2 = false;
			wind = new Vector3 (Random.Range (-10f, 10f) * (count / 3), Random.Range (-10f, 10f) * (count / 3), 0);
		} else if (count % 3 != 0) {
			flag2 = true;
		}
	}

	public void Count (int s) {
		score += s;
	}

	public bool TextMove () {
		return true;
	}

	public void Stop () {
	}

	public void Continue () {
	}

	public void Restart() {
		EditorSceneManager.LoadScene ("Shooting");
	}

	public void GameOver() {
		
	}
}
