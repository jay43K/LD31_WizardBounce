using UnityEngine;
using System.Collections;

public class HoverText : MonoBehaviour {
	public tk2dUIItem button;
	public TextFade fadeControl;
	// Use this for initialization
	void Start () {
		button.OnHoverOver += HoverOver;
		button.OnHoverOut += HoverOut;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void HoverOver () {
		fadeControl.defaultFadeIn = true;
	}
	
	void HoverOut () {
		fadeControl.defaultFadeOut = true;
	}
}