using UnityEngine;
using System.Collections;

public class TextHeldTime : MonoBehaviour {
	public PlayerControl player;
	public tk2dTextMesh text;

	private int score;
	// Use this for initialization
	void Start () {
		text = GetComponent<tk2dTextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		//text.text = "Held Time: " + player.heldTime.ToString() + "\n" + "Jumps " + player.successfulJumps.ToString();

		if (player != null) {
			int jumps = player.successfulJumps;
			int tricks = player.underwearTrick;
			
			if (jumps > 0) {
				score = jumps;
				if (tricks > 0) {
					score = jumps * tricks;
				}
			}
			
			text.text = score.ToString();
			text.Commit();
		}

	}
}
