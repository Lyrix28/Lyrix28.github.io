using UnityEngine;
using System.Collections;

public class MonsterCollider : MonoBehaviour
{
	public FirstController sceneController;
	// Use this for initialization
	void Start ()
	{
		sceneController = SSDirector.Instance.currentSceneController as FirstController;
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.name.Contains ("Plane"))
			return;

		if (c.gameObject.name.Contains ("Garen")) {
			gameObject.GetComponent<MonsterData> ().speed = 2f;

		}

    }

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.name.Contains ("Garen")) {
			GameEventManager.Instance.GameOver ();
		} else {
			var vec = gameObject.GetComponent<MonsterData> ().direction;
			gameObject.GetComponent<MonsterData> ().direction = -vec;
		}

	}
}

