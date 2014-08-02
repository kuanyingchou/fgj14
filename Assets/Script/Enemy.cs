using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Weapon" && Player.player_status == Player.Player_Status.Attack)
			Destroy(gameObject);
	}
}
