using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	bool spotted = false;
	int speed=3;
	public int jumpForce = 450;
	public int glideJumpForce = 550;
	public int glidingForce = 10;

	float attack_time_limit = 0.2f;
	float attack_CDtime_limit = 0.2f;
	float attacking_time, attack_CDtime;

	float gliding_time_limit = 0.2f;
	float gliding_time;

	string player_name;
	Animator anim;

	string soundPath = "Music/";
	AudioClip audioClip;

	public enum Player_Status {Moving, Jump, Jump2, Attack, Gliding, Gliding_Jump, Over, Dead, Pass};
    public static Player_Status player_status = Player_Status.Moving;
	

	void Awake(){
		player_status = Player_Status.Moving;
	}

	void Start () {
		player_status = Player_Status.Moving;
		player_name = gameObject.name;
		anim = gameObject.GetComponent<Animator>();

		Physics2D.gravity = new Vector3(0, -9.8f, 0);
	}
	
	void Update () {
		switch(player_status){
		case Player_Status.Dead:
			break;
		case Player_Status.Over:
			break;
		case Player_Status.Pass:
			break;
		case Player_Status.Attack:
			AttackCheck();
			PlayerFunction ();
			break;
		case Player_Status.Gliding:
			PlayerFunction ();
			break;
		case Player_Status.Gliding_Jump:
			PlayerFunction ();
			GlidingJumpCheck();
			break;
		default:
			PlayerFunction ();
			break;
		}
		anim.SetFloat("State", (float)player_status);
	}

	void FixedUpdate (){
	
		switch (player_name) {
		case "Hero1":
			break;
		case "Hero2":
			break;
		case "Hero3":
			if(attack_CDtime > 0)
				attack_CDtime -= Time.deltaTime;
			break;
		case "Hero4":
			break;
		case "Hero5":
			break;
		default:
			break;
		}


		switch(player_status){
		case Player_Status.Dead:
//gooku			Application.LoadLevel(Application.loadedLevel);
			GameManger.game_status = GameManger.Game_Status.Over;
			AudioPlay("fail");
			player_status = Player_Status.Over;
			Destroy(rigidbody2D);
			break;
		case Player_Status.Over:
			break;
		case Player_Status.Pass:
			GameManger.game_status = GameManger.Game_Status.Pass;
			break;
		case Player_Status.Attack:
//gooku			PlayerFunction ();
			Movement ();
			break;
		default:
//gooku			PlayerFunction ();
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
			if(player_status != Player_Status.Attack && attack_CDtime <= 0)
				Attack();
			break;
		case "Hero4":
			if(player_status != Player_Status.Gliding){
				Glide();
			}else{
				GlidingHandle();
			}
			break;
		case "Hero5":
			AntiGravity();
			break;
		default:
			break;
		}
	}

	void Jump(){
		if (player_status == Player_Status.Jump) {
			rigidbody2D.velocity = Vector2.zero;
			player_status = Player_Status.Jump2;
			AudioPlay("jump2");
		}
		else{
			player_status = Player_Status.Jump;
			AudioPlay("jump1");
		}

		rigidbody2D.AddForce (transform.up*jumpForce);
	}

	void Attack(){
//		if (attacking_time > 0)
//			return;
		player_status = Player_Status.Attack;
		attacking_time  = attack_time_limit;
		AudioPlay("attack");
	}

	void AttackCheck(){
		attacking_time -= Time.deltaTime;
		
		if (attacking_time <= 0) {
//			attacking_time = 0;
			player_status = Player_Status.Moving;
			attack_CDtime = attack_CDtime_limit;
		}
	}

	void Glide(){
		rigidbody2D.AddForce (transform.up*glideJumpForce);
		player_status = Player_Status.Gliding;
		AudioPlay("glide1");
	}

	void GlidingHandle(){
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce (transform.up*glidingForce);
		player_status = Player_Status.Gliding_Jump;
		AudioPlay("glide2");
	}

	void GlidingJumpCheck(){
		if (rigidbody2D.velocity.y < 0)
			player_status = Player_Status.Gliding;
	}

	void AntiGravity(){
		Physics2D.gravity = -Physics2D.gravity;
		transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 0);
		AudioPlay("anti");
	
	}
	
	void OnCollisionEnter2D(Collision2D coll) {

		if(player_status == Player_Status.Jump 
		   || player_status == Player_Status.Jump2
		   || player_status == Player_Status.Gliding
		   ){
			if (coll.gameObject.tag == "Floor") {
				player_status = Player_Status.Moving;	
//				anim.SetBool("Sheep_fail", false);
			}
		}

		if (coll.gameObject.tag == "Needle") {
			player_status = Player_Status.Dead;
		}

		if (coll.gameObject.tag == "Enemy" && player_status != Player_Status.Attack) {
			player_status = Player_Status.Dead;
		}

		if (coll.gameObject.tag == "Goal") {
			player_status = Player_Status.Pass;
		}
	}

	void AudioPlay(string audioName){
		if(audio){
			Resources.UnloadAsset((Object)audioClip);
			audioClip = (AudioClip)Resources.Load(soundPath + audioName);
			audio.clip = audioClip;
			audio.Play();
		}
		else{
			print ("Hero has no audioSource!");
		}
	}
/*
	void Dead(){

		if (player_status == Player_Status.Attack)
			return;

			player_status = Player_Status.Dead;
	}
*/
}
