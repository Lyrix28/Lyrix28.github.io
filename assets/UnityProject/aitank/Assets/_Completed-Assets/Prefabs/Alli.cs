using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alli: MonoBehaviour {

	private NavMeshAgent man;
	public Transform target;

	private int count = 0;
	// Use this for initialization
	void Start () {
		man = gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
			return;
		man.SetDestination (target.position);

		if (Vector3.Distance(target.position, transform.position) > 20)
			return;
		
		count = count + 1;
		if (count < 10)
			return;
		count = 0;

		Ray ray = new Ray(transform.position, target.position);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			Debug.Log (hit.transform.gameObject.name);
			if (!(hit.transform.gameObject.name == target.gameObject.name))
				return;
		}

		Complete.TankShooting mono = gameObject.GetComponent<Complete.TankShooting> ();
		Rigidbody shellInstance =
			Instantiate (mono.m_Shell, mono.m_FireTransform.position, mono.m_FireTransform.rotation) as Rigidbody;

		shellInstance.velocity = mono.m_MinLaunchForce * mono.m_FireTransform.forward;
		mono.m_ShootingAudio.clip = mono.m_FireClip;
		mono.m_ShootingAudio.Play ();
	}
}