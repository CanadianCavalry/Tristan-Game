using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float moveSpeed;
	public float currentSpeed;
	private float moveX;
	private float moveY;

	private bool canJump;
	public float jumpForce;

	public SpriteRenderer playerSprite;
	private Rigidbody2D rigidBody;
	private Animator anim;
	

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody2D> ();

		//Initialize attribute values
		currentSpeed = moveSpeed;
		canJump = true;
	}
	
	void Update () {
		//Get inputs
		float moveInput = Input.GetAxis ("Horizontal");
		if (moveInput == 0) {
			anim.SetTrigger ("Idle");
		} else {
			anim.SetTrigger ("Run");
		}

		//Movement
		if (moveInput > 0.0f) {
			moveX = 1.0f;
		} else if (moveInput < 0.0f) {
			moveX = -1.0f;
		} else {
			moveX = 0.0f;
		}

		//Jumping
		bool jumpInput = Input.GetButtonDown ("Jump");
		if (jumpInput && canJump) {
			canJump = false;
			moveY = jumpForce;
		} else {
			moveY = rigidBody.velocity.y;
			if (rigidBody.velocity.y == 0.0f) {
				canJump = true;
			}
		}

		//Player Facing
		if (playerSprite.flipX == true && moveX < 0.0f) {
			flipPlayer ();
		} else if (playerSprite.flipX == false && moveX > 0.0f) {
			flipPlayer ();
		}

		rigidBody.velocity = new Vector2 (moveX * currentSpeed, moveY);
	}

	private void flipPlayer() {
		playerSprite.flipX = !playerSprite.flipX;
	}
}
