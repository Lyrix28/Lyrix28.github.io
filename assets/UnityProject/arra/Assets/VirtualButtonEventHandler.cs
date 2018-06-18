using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler {

	public GameObject vb;
	public Animator ani;

	// Use this for initialization
	void Start () {
		VirtualButtonBehaviour vbb = vb.GetComponent<VirtualButtonBehaviour> ();

		if (vbb) {
			vbb.RegisterEventHandler (this);
		}
	}
	
	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) {
		
		Debug.Log ("Pressed");
	}

	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) {
		Debug.Log ("Released");
	}
}
