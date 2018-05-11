using UnityEngine;

public class GameEventManager {

	private static GameEventManager instance { get; set; }
	public static GameEventManager Instance { get { return instance ?? (instance = new GameEventManager()); } }

	public delegate void ScoreEvent();
	public static event ScoreEvent scoreEvent;

	public delegate void GameOverEvent();
	public static event GameOverEvent gameOverEvent;

	public void OnLostGoal() {
		if (scoreEvent != null) {
			scoreEvent ();
		}
	}

	public void GameOver() {
		if (gameOverEvent != null) {
			gameOverEvent ();
		}
	}
}
