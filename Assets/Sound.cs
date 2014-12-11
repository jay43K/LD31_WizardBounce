using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public static Sound board;

	public AudioSource charge;
	public AudioSource death;
	public AudioSource jump;
	public AudioSource trick;
	public AudioSource end;

	public bool playTheme = true;
	public AudioSource theme;
	public bool playOnce = true;

	public bool testCharge = false;
	public bool testDeath = false;
	public bool testJump = false;
	public bool testTrick = false;
	public bool testEnd = false;

	public bool endGame = false;

	void Awake () {
		DontDestroyOnLoad(this.gameObject);
	}
	 
	void Start () {
		board = this;

		if (Application.loadedLevel == 0) {
			Application.LoadLevel(1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel == 1 && playOnce && playTheme) {
			theme.Play();
			playOnce = false;
		}

		if (testCharge) {
			Charge ();
			testCharge = false;
		}

		if (testDeath) {
			Death ();
			testDeath = false;
		}

		if (testJump) {
			Jump ();
			testJump = false;
		}

		if (testTrick) {
			Trick ();
			testTrick = false;
		}

		if (testEnd) {
			End ();
			testEnd = false;
		}

	}

	public void Charge () {
		charge.PlayOneShot(charge.clip);
	}

	public void Death () {
		death.PlayOneShot(death.clip);
	}

	public void Jump () {
		jump.PlayOneShot(jump.clip);
	}

	public void Trick() {
		trick.PlayOneShot(trick.clip);
	}

	public void End() {
		theme.Stop();
		end.PlayOneShot(end.clip);
	}
}
