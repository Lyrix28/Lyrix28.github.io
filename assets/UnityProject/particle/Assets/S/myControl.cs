using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myControl : MonoBehaviour {

	public GameObject smokel;
	public GameObject smoker;
	public GameObject broke;

	public GameObject firel;
	public GameObject firer;

	bool running = false;
	Rigidbody rig;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
		firel.GetComponent<ParticleSystem> ().Stop ();
		firer.GetComponent<ParticleSystem> ().Stop ();
		broke.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		float x = rig.velocity.x;
		float z = rig.velocity.z;
		float s = x * x + z * z;
		if (s > 5) {
			smokel.SetActive (true);
			smoker.SetActive (true);
		} else {
			smokel.SetActive (false);
			smoker.SetActive (false);
		}
		if (Input.GetButtonDown ("Vertical")) {
			if (!running) {
				running = true;
				firel.GetComponent<ParticleSystem> ().Play ();
				firer.GetComponent<ParticleSystem> ().Play ();
			}
		}
		if (Input.GetButtonDown ("Vertical"))
			running = false;
		Debug.Log (rig.velocity);
	}
}
