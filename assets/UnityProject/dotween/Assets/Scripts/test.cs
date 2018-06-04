using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DOMove (new Vector3 (2, 2, 2), 2)
			.SetComplete ((a)=>{Debug.Log("Complete Move");});
		
		transform.DOScale (new Vector3 (2, 2, 2), 2)
			.SetComplete ((a)=>{Debug.Log("Complete Scale");});

		transform.DORotate (new Vector3 (0, 360, 0), 2)
			.SetComplete ((a)=>{Debug.Log("Complete Rotate");})
			.SetLoops(10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
