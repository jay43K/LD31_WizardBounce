using UnityEngine;
using System.Collections;

public class SpriteFade : MonoBehaviour {

	public tk2dSprite sprite;

	public bool defaultFadeIn = false;
	public bool defaultFadeOut = false;
	
	public float autoInDuration = 1.0f;
	public float autoOutDuration = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (defaultFadeIn) {
			FadeIn (autoInDuration);
			defaultFadeIn = false;
		}
		
		if (defaultFadeOut) {
			FadeOut (autoOutDuration);
			defaultFadeOut = false;
		}
	}

	public void FadeIn (float duration) {
		StartCoroutine ("DoFadeIn", duration);
	}
	
	public void FadeOut (float duration) {
		StartCoroutine ("DoFadeOut", duration);
	}
	
	IEnumerator DoFadeIn (float duration) {
		//Debug.Log ("Fade in worked with " + duration);
		float elapsed = 0.0f;
		
		while (elapsed < duration) {
			elapsed += Time.smoothDeltaTime;
			
			float percentComplete = elapsed / duration;
			Color col = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			col.a = Mathf.Lerp(0.0f, 0.5f, percentComplete);
			sprite.color = col;
			yield return null;
		}
	}
	
	IEnumerator DoFadeOut (float duration) {
		float elapsed = 0.0f;
		
		while (elapsed < duration) {
			elapsed += Time.smoothDeltaTime;
			
			float percentComplete = elapsed / duration;
			Color col = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			col.a = Mathf.Lerp(1.0f, 0.0f, percentComplete);
			sprite.color = col;
			yield return null;
		}
	}
}
