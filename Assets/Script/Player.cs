using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	int speed=1;
	int jumpForce = 450;
	string player_name;

	public enum Player_Status {Moving, Jump, Jump2, Dead, Pass};
	Player_Status player_status = Player_Status.Moving;

	void Start () {
		player_status = Player_Status.Moving;
		player_name = gameObject.name;
	}
	
	void Update () {
	}

	void FixedUpdate (){

		switch(player_status){
		case Player_Status.Dead:
			break;
		case Player_Status.Pass:
			break;
		default:
			PlayerFunction ();
			Movement ();
			break;
		}
	}

	void Movement(){
		transform.Translate (Vector2.right * speed * Time.deltaTime);
	}
	
	void PlayerFunction(){

		if (!Input.GetKeyDown ("space"))
			return;

		switch (player_name) {
		case "Hero1":
			if(player_status == Player_Status.Jump)
				break;
			Jump();
			break;
		case "Hero2":
			if(player_status == Player_Status.Jump2)
				break;
			Jump();
			break;
		case "Hero3":
			break;
		case "Hero4":
			break;
		case "Hero5":
			break;
		default:
			break;
		}

	}

	void Jump(){
		rigidbody2D.AddForce (transform.up*jumpForce);

		if(player_status == Player_Status.Jump)
			player_status = Player_Status.Jump2;
		else
			player_status = Player_Status.Jump;
	}



	void OnCollisionEnter2D(Collision2D coll) {

		if(player_status == Player_Status.Jump || player_status == Player_Status.Jump2){
			if (coll.gameObject.tag == "Floor") {
				player_status = Player_Status.Moving;	
//				anim.SetBool("Sheep_fail", false);
			}
		}

		if (coll.gameObject.tag == "Needle") {
			player_status = Player_Status.Dead;
		}

		if (coll.gameObject.tag == "Goal") {
			player_status = Player_Status.Pass;
		}
	}

}
