using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	int speed=1;
	int jumpForce = 450;
	float attack_time_limit = 0.2f;
	float attack_CDtime_limit = 0.5f;
	float attacking_time, attack_CDtime;
	string player_name;
	Animator anim;

	public enum Player_Status {Moving, Jump, Jump2, Attack, Dead, Pass};
	Player_Status player_status = Player_Status.Moving;

	void Start () {
		player_status = Player_Status.Moving;
		player_name = gameObject.name;
//		anim = gameObject.GetComponent<Animator>();
	}
	
	void Update () {
	}

	void FixedUpdate (){

		switch(player_status){
		case Player_Status.Dead:
			Application.LoadLevel(Application.loadedLevel);
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
			if(player_status != Player_Status.Attack){
				if(attack_CDtime > 0)
				{
					attack_CDtime -= Time.deltaTime;
				}else{
					Attack();
				}
			}

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

	void Attack(){
		if (attacking_time == 0) {
			player_status = Player_Status.Attack;
			attacking_time  = attack_time_limit;
		}
		attacking_time -= Time.deltaTime;

		if (attacking_time <= 0) {
			player_status = Player_Status.Moving;
			attack_CDtime = attack_CDtime_limit;
		}
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
