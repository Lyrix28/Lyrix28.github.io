using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension {

	public static IEnumerator DOMove(this MonoBehaviour mono, Tween tween)
	{
		Vector3 speed = (tween.target - tween.transform.position) / tween.duration;
		int times = 0;
		while (times++ < tween.times) {
			for (float f = tween.duration; f >= 0.0f; f -= Time.deltaTime)
			{
				tween.transform.Translate(speed * Time.deltaTime);
				yield return null;
				while (tween.isPause == true)
				{
					yield return null;
				}
			}
		}

		tween.OnComplete();
	}

	public static Tween DOMove(this Transform transform, Vector3 target, float duration)
	{
		MonoBehaviour mono = transform.GetComponent<MonoBehaviour>();
		Tween tween = new Tween(transform, target, duration);
		Coroutine coroutine = mono.StartCoroutine(mono.DOMove(tween));
		tween.SetCoroutine(coroutine);
		return tween;
	}

	public static IEnumerator DOScale(this MonoBehaviour mono, Tween tween)
	{
		Vector3 speed = (tween.target - tween.transform.localScale) / tween.duration;
		int times = 0;
		while (times++ < tween.times) {
			for (float f = tween.duration; f >= 0.0f; f -= Time.deltaTime) {
				tween.transform.localScale += speed*Time.deltaTime;
				yield return null;
				while (tween.isPause == true) {
					yield return null;
				}
			}
		}
		tween.OnComplete();
	}

	public static Tween DOScale(this Transform transform, Vector3 target, float time)
	{
		MonoBehaviour mono = transform.GetComponent<MonoBehaviour>();
		Tween tween = new Tween(transform, target, time);
		Coroutine coroutine = mono.StartCoroutine(mono.DOScale(tween));
		tween.SetCoroutine(coroutine);
		return tween;
	}

	public static IEnumerator DORotate(this MonoBehaviour mono, Tween tween)
	{
		Vector3 speed = (tween.target - tween.transform.localScale) / tween.duration;
		int times = 0;
		while (times++ < tween.times) {
			for (float f = tween.duration; f >= 0.0f; f -= Time.deltaTime) {
				tween.transform.Rotate (speed * Time.deltaTime);
				yield return null;
				while (tween.isPause == true) {
					yield return null;
				}
			}
		}
		tween.OnComplete();
	}

	public static Tween DORotate(this Transform transform, Vector3 target, float time)
	{
		MonoBehaviour mono = transform.GetComponent<MonoBehaviour>();
		Tween tween = new Tween(transform, target, time);
		Coroutine coroutine = mono.StartCoroutine(mono.DORotate(tween));
		tween.SetCoroutine(coroutine);
		return tween;
	}

}