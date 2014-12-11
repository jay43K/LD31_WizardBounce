using UnityEngine;
using System.Collections;

public class SpriteMover : MonoBehaviour {

	public float speed = 10.0f;
	public MovePoint[] movePoints;
	public Vector3 startPoint;
	public int position = 0;
	public bool go = false;

	public bool autoRun = false;

	void Awake () {
		startPoint = transform.position;
		movePoints = gameObject.GetComponentsInChildren<MovePoint>();
	}

	void Update () {
		if (go) {
			float motion = Time.deltaTime * speed;
		
			Vector3 movePos = movePoints[position].transform.position;

			transform.position = Vector3.MoveTowards(transform.position, movePos, motion);

			if (transform.position == movePos) {
				go = false;
				position++;
				if (autoRun) {
					go = true;
				}
			}
		}

		if (position == movePoints.Length) {
			position = 0;
		}
	}

	public int GetPosition () {
		return position;
	}

	public void SetPosition (int pos) {
		if (pos <= movePoints.Length) {
			position = pos;		
		}
	}

	public void GoNext () {
		go = true;
	}
	
}
