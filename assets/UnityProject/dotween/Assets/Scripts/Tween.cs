using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween {

	public Transform transform;
	public Action<Tween> complete;
	public Coroutine coroutine;
	public int times;
	public Vector3 target;
	public float duration;
	public bool isPause;

	public Tween(Transform transform, Vector3 target, float duration) {
		this.transform = transform;
		this.target = target;
		this.duration = duration;
		this.isPause = false;
		this.times = 1;
	}

	public Tween SetComplete(Action<Tween> ac) {
		this.complete += ac;
		return this;
	}

	public Tween SetCoroutine(Coroutine coroutine) {
		this.coroutine = coroutine;
		return this;
	}

	public Tween SetLoops(int num) {
		this.times = num;
		return this;
	}

	public Tween Pause() {
		this.isPause = true;
		return this;
	}

	public Tween Continue() {
		this.isPause = false;
		return this;
	}

	public Tween Stop() {
		this.transform.GetComponent<MonoBehaviour> ().StopCoroutine (this.coroutine);
		return this;
	}

	public void OnComplete() {
		if (this.complete != null)
			this.complete (this);
	}
}