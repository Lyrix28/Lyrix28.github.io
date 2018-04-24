using UnityEngine;
using System.Collections;

public class ArrowCollider : MonoBehaviour
{
	public ShootingController sceneController;
	// Use this for initialization
	void Start ()
	{
		sceneController = SSDirector.Instance.currentSceneController as ShootingController;
	}

	void OnTriggerEnter(Collider c) {  
		gameObject.transform.parent.transform.GetComponent<Rigidbody>().isKinematic = true;  
		gameObject.SetActive(false);
		int score = 5 - int.Parse (c.gameObject.name);
		sceneController.Count (score);
    }
}

