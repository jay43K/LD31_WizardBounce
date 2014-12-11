using UnityEngine;
using System.Collections;

public class ScreenFlash : MonoBehaviour {
	
	public tk2dSprite theFlash;
	
	public Color effect;
	
	public float startTime;
	public float duration = 0.6f;
	public float shortDuration = 0.3f;

	public bool shortFlash = false;

	public bool fire = false;
	// Use this for initialization


	public void Fire () {
		StartCoroutine("DoFlash");
	}

	void Start () {
		if (shortFlash) {
			duration = shortDuration;
		}

		StartCoroutine("DoFlash");
		StartCoroutine("FlashKill");
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		// old flash code
		//float t = (Time.time - startTime) / duration;
		//effect.a = Mathf.Lerp(1.0f, 0.0f, t);
		//theFlash.color = effect;

		// toggle option
		if (fire) {
			Fire ();
			fire = false;
		}
	}

	IEnumerator DoFlash () {
		float elapsed = 0.0f;

		while (elapsed < duration) {

			elapsed += Time.smoothDeltaTime;

			float percentComplete = elapsed / duration;
			effect.a = Mathf.Lerp(1.0f, 0.0f, percentComplete);
			theFlash.color = effect;
			yield return null;
		}
		//float t = (Time.time - startTime) / duration;
		//effect.a = Mathf.Lerp(1.0f, 0.0f, t);
		//theFlash.color = effect;
		//yield return new WaitForSeconds(duration);
	}
	
	IEnumerator FlashKill () {
		float destroy = duration + 1.0f;
		yield return new WaitForSeconds(destroy);
		Destroy(this.gameObject);
	}
}