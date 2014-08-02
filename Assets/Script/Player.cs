using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	int speed=1;
	int jumpForce = 450;

	public enum Player_Status {Moving, Jump, Stop};
	Player_Status player_status = Player_Status.Moving;

	void Start () {
		player_status = Player_Status.Moving;
	}
	
	void Update () {
		PlayerFunction ();
	}

	void FixedUpdate (){
		Movement ();
	}

	void Movement(){
		transform.Translate (Vector2.right * speed * Time.deltaTime);
	}
	
	void PlayerFunction(){

		if (!Input.GetKeyDown ("space"))
			return;

		if(player_status == Player_Status.Jump)
			return;
	
		Jump();
	}

	void Jump(){
		rigidbody2D.AddForce (transform.up*jumpForce);
		player_status = Player_Status.Jump;
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if(player_status == Player_Status.Jump){
			if (coll.gameObject.tag == "Floor") {
				player_status = Player_Status.Moving;	
//				anim.SetBool("Sheep_fail", false);
			}
		}
		
	}

}
