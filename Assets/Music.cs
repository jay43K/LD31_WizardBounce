using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	public bool playOnce = true;
	private AudioSource aud;
	// Use this for initialization

	void Awake () {
		aud = GetComponent<AudioSource>();
	}
	void Start () {
		DontDestroyOnLoad(this.gameObject);


	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel == 1 && playOnce) {
			aud.Play();
			playOnce = false;
		}
	}
}
