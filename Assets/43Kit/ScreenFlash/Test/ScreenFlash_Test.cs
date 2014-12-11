using UnityEngine;
using System.Collections;

public class ScreenFlash_Test : MonoBehaviour {

	public GameObject ScreenFlashFab;
	public bool fire = false;
	public bool shortFlash = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fire) {
			GameObject theFlash = Instantiate(ScreenFlashFab) as GameObject;
			if (shortFlash) {
				theFlash.GetComponent<ScreenFlash>().shortFlash = true;
			}
			fire = false;
		}

		// todo
		// on camera position
	}
}
