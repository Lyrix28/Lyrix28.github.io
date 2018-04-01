using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ss_main : MonoBehaviour {
	public Transform mercury, venus, earth, mars, jupiter, saturn, uranus, neptune;
	public Vector3 mercury_v, venus_v, earth_v, mars_v, jupiter_v, saturn_v, uranus_v, neptune_v;

	// Use this for initialization
	void Start () {
		mercury_v = get ();
		venus_v = get ();
		earth_v = get ();
		mars_v = get ();
		jupiter_v = get ();
		saturn_v = get ();
		uranus_v = get ();
		neptune_v = get ();
	}

	Vector3 get() {
		return new Vector3 (0, Random.Range(1,360), Random.Range(1,360));
	}

	// Update is called once per frame
	void Update () {
		mercury.RotateAround (Vector3.zero, mercury_v, 47 * Time.deltaTime);
		venus.RotateAround (Vector3.zero, venus_v, 35 * Time.deltaTime);
		earth.RotateAround (Vector3.zero, earth_v, 30 * Time.deltaTime);
		mars.RotateAround (Vector3.zero, mars_v, 24 * Time.deltaTime);
		jupiter.RotateAround (Vector3.zero, jupiter_v, 13 * Time.deltaTime);
		saturn.RotateAround (Vector3.zero, saturn_v, 9 * Time.deltaTime);
		uranus.RotateAround (Vector3.zero, uranus_v,  6 * Time.deltaTime);
		neptune.RotateAround (Vector3.zero, neptune_v, 5 * Time.deltaTime);
		transform.Rotate (Vector3.up * Time.deltaTime * 50);
        earth.Rotate (Vector3.up * Time.deltaTime * 250);
        mercury.Rotate (Vector3.up * Time.deltaTime * 300);
        venus.Rotate (Vector3.up * Time.deltaTime * 280);
        mars.Rotate (Vector3.up * Time.deltaTime * 220);
        jupiter.Rotate (Vector3.up * Time.deltaTime * 180);
        saturn.Rotate (Vector3.up * Time.deltaTime * 160);
        uranus.Rotate (Vector3.up * Time.deltaTime * 150);
        neptune.Rotate (Vector3.up * Time.deltaTime * 140);
	}
}
