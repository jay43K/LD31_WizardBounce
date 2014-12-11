using UnityEngine;
using System.Collections;

public class TextFade : MonoBehaviour {

	public tk2dTextMesh text;

	public bool defaultFadeIn = false;
	public bool defaultFadeOut = false;

	public float autoInDuration = 1.0f;
	public float autoOutDuration = 1.0f;

	public Color fadeInColor;

	void Start () {
	
	}
	
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

	// public method to call Fade In routine.  
	//Pass in duration?  
	//Probably would be better for handler class. 
	//Can include self running values.
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
			//Color col = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			Color col = fadeInColor;
			col.a = Mathf.Lerp(0.0f, 1.0f, percentComplete);
			text.color = col;
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
			text.color = col;
			yield return null;
		}
	}
}
