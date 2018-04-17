using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	public int round = 1;
	public bool onFlying = false;
	public bool onRunning = false;

	public int score = 0;

	private int count = 0;

	public bool flag = false;

	public SSActionManager actionManager { get; set; }

	void Awake() {
		round = 1;
		SSDirector director = SSDirector.Instance;
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
	}

	public void LoadResources() {
		
	}

	void OnGUI() {
		if ( score % 10 == 0 && score != 0)
			Upgrade ();
		GUI.color = Color.green;
		GUI.Label (new Rect (750, 0, 60, 50), "Round: "+round.ToString());
		GUI.Label (new Rect (750, 50, 60, 50), "Score: "+score.ToString());
	}

	void Upgrade() {
		if (round >= 6)
			GameOver ();
		else {
			flag = false;
			score += 2;
			round += 1;
		}
	}

	public bool TextMove () {
		if (++count > 100) {
			count = 0;
			onFlying = true;
		}
		return onFlying;
	}

	public void Stop () {
		onRunning = false;
	}

	public void Continue () {
		onRunning = true;
	}

	public void Restart() {
		EditorSceneManager.LoadScene ("Main");
		onRunning = true;
	}

	public void GameOver() {
		
	}
}
