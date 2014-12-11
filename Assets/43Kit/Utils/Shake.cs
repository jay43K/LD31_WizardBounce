using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {
	public float duration = 0.5f;
	public float magnitude = 0.1f;
	
	public bool fire = false;
	public bool autoFire = false;

	public bool useSide = true;
	public bool useVert = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fire) {
			Fire ();
			fire = false;
		}
	}
	
	public void Fire () {
		StartCoroutine ("DoShake");
	}
	
	IEnumerator DoShake () {
		float elapsed = 0.0f;
		
		Vector3 originalTransformPos = transform.position;
		float x = 0.0f;
		float y = 0.0f;
		
		while (elapsed < duration) {
			elapsed += Time.deltaTime;
			
			float percentageComplete = elapsed / duration;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentageComplete - 3.0f, 0.0f, 1.0f);


			if (useSide) {
				x = Random.value * 2.0f - 1.0f;
				x *= magnitude * damper;
			}

			if (useVert) {
				y = Random.value * 2.0f - 1.0f;
				y *= magnitude * damper;
			}
			
				// add y back in
			transform.position = new Vector3(x + originalTransformPos.x,y + originalTransformPos.y, originalTransformPos.z);
			
			yield return null;
		}
		
		transform.position = originalTransformPos;
	}
}
