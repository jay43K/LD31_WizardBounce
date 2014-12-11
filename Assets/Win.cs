using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	public GameObject player;
	public TextFade thanksFade;
	public TextFade madeItFade;
	public SpriteFade bgFade;

	public bool winOnce = true;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D () {
		Debug.Log ("Win!");

		if (player != null) {
			Destroy(player);
		}

		if (winOnce) {
			if (Sound.board != null) {
				Sound.board.End ();
			}

			StartCoroutine("BGFade");
			winOnce = false;
		}
	}

	IEnumerator BGFade () {
		StartCoroutine("MadeItFade");
		bgFade.defaultFadeIn = true;
		yield return null;
	}

	IEnumerator ThanksFade () {
		yield return new WaitForSeconds(1.0f);
		thanksFade.defaultFadeIn = true;

	}

	IEnumerator MadeItFade () {
		yield return new WaitForSeconds(1.0f);
		madeItFade.defaultFadeIn = true;
		StartCoroutine("ThanksFade");
	}


}
