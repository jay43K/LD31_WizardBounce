using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	//test vars
	public float timeSinceAbleToJump = 0f;
	
	// jump state vars
	public int successfulJumps = 0;
	public bool wrecking = false;
	public bool dead = false;

	// dead state stuff
	public BoxCollider2D bounceCollider;
	public BoxCollider2D playerCollider;
	
	// jump mechanic vars
	public float heldTime = 0f;
	public float multiplier = 0f;

	// in air stuff
	public bool doingAMove = false;
	public int underwearTrick = 0;
	
	// when touching / on the ground
	public Transform canJumpPoint;
	public LayerMask theSurface;
	public bool canJump = false;

	// sprite
	//public SpriteRenderer sprite;
	public tk2dSprite sprite;
	public const string playerStand = "Player_Stand_0";
	public const string playerMovingUp = "Player_MovingUp_0";
	public const string playerMovingDown = "Player_MovingDown_0";
	public const string playerCrouch = "Player_Crouch_0";

	//effects
	public GameObject effects;
	public tk2dSpriteAnimator animator;
	public tk2dSpriteAnimator playerAnimator;

	void Awake () {
		//sprite = GetComponent<SpriteRenderer>();
		sprite = GetComponent<tk2dSprite>();
		playerAnimator = GetComponent<tk2dSpriteAnimator>();
		playerCollider = GetComponent<BoxCollider2D>();
	}

	void Start () {
		animator = effects.GetComponent<tk2dSpriteAnimator>();
	}
	
	
	void Update () {


		canJump = Physics2D.OverlapCircle(canJumpPoint.position, 0.1f, theSurface);

		if (canJump) {
			timeSinceAbleToJump += Time.deltaTime;
			sprite.SetSprite( playerStand );
		}
		else {
			timeSinceAbleToJump = 0f;
			heldTime = 0f;
		}
	
		
		// Hold Jump
		if (Input.GetButton("Fire1") && canJump && !wrecking) {
			heldTime += Time.deltaTime;
			sprite.SetSprite( playerCrouch );

			if (Sound.board != null) {
				Sound.board.Charge();
			}

			effects.SetActive(true);

			if (heldTime >= 0.59f) {

			}
			else if(heldTime >= 0.39f) {
				animator.Play("Blood");
			}
			else {
				animator.Play("Lightning");
			}
			//Debug.Log ("Holding " + heldTime);
		}

		/* In Air Jump Attempt Wreck
		if (Input.GetButtonDown ("Fire1") && !canJump) {
			TimingWreck();
		}
		*/


		// Let Go of Jump
		if (Input.GetButtonUp ("Fire1") && !wrecking) {
			effects.SetActive(false);
			Debug.Log (heldTime + " jump seconds");


			if (heldTime >= 0.6f) {
				float mult = multiplier * 10f;
				mult = Mathf.Clamp(mult, 0, 200f);
				rigidbody2D.AddForce(Vector2.up * (800f + mult));
				if (Sound.board != null) {
				Sound.board.Jump ();
					
				}
				successfulJumps++;
			}
			else if(heldTime >= 0.4f) {
				float mult = multiplier * 10f;
				mult = Mathf.Clamp(mult, 0, 200f);
				rigidbody2D.AddForce(Vector2.up * (600f + mult));
				if (Sound.board != null) {
				Sound.board.Jump ();
				
				}
				successfulJumps++;
			}
			else if(heldTime >= 0.15f) {
				float mult = multiplier * 10f;
				mult = Mathf.Clamp(mult, 0, 200f);
				rigidbody2D.AddForce(Vector2.up * (400f + mult));
				// text max height
				//rigidbody2D.AddForce(Vector2.up * (1000f));
				if (Sound.board != null) {
					Sound.board.Jump ();
				}
				timeSinceAbleToJump = 0f;
				successfulJumps++;
			}
			heldTime = 0f;
		}

		// Check Jump State
		if (heldTime >= 0.38f && successfulJumps == 0) {
			// wreck
			Debug.Log ("OVER HELD WRECK!");
			heldTime = 0.0f;
			wrecking = true;
			StartCoroutine("Wreck");
		}

		if (successfulJumps >= 1 && timeSinceAbleToJump >= 0.8f) {
			TimingWreck();
		}

	



		// Velocity changes sprite
		if (rigidbody2D.velocity.y > 0.5f & !doingAMove) {
			sprite.SetSprite( playerMovingUp );
		}

		if (rigidbody2D.velocity.y < 0.5f && rigidbody2D.velocity.y != 0f && !doingAMove && !canJump) {
			sprite.SetSprite( playerMovingDown );
		}

		// TRICKS || "first trick"
		if (!canJump && Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown ("Fire2") && !doingAMove) {
			//Debug.Log ("Do Left Move");
			//sprite.color = Color.blue;
			if (Sound.board != null) {
				Sound.board.Trick();
			}
			underwearTrick++;
			doingAMove = true;
			playerAnimator.Play ("FirstTrick");
			StartCoroutine("StopFirstTrick");
		}

		multiplier = successfulJumps + underwearTrick;

		if (doingAMove && canJump && !wrecking) {
			Debug.Log ("Doing a trick while landing! Wreck!");
			TimingWreck();
		}

		if (dead) {
			rigidbody2D.AddTorque(5.0f);
		}
		if (transform.position.y <= -13f) {
			Application.LoadLevel(1);
		}
	}

	void TimingWreck () {
		Debug.Log ("Timing WRECK!");
		heldTime = 0.0f;
		wrecking = true;
		StartCoroutine("Wreck");
	}

	IEnumerator StopFirstTrick () {
		yield return new WaitForSeconds(0.8f);
		//sprite.color = Color.white;
		doingAMove = false;
	}

	// all wrecks come here
	IEnumerator Wreck () {
		if (Sound.board != null) {
			Sound.board.Death ();
		}
		successfulJumps = 0;
		effects.SetActive(false);
		Dying ();
		StartCoroutine("TestWreckAnim");
		yield return new WaitForSeconds(1.5f);
		wrecking = false;
	}

	void Dying () {
		Destroy(playerCollider);
		Destroy(bounceCollider);
		dead = true;
	}

	IEnumerator TestWreckAnim () {
		sprite.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		sprite.color = Color.white;
	}

}
