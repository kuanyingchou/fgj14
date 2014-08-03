using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    IEnumerator Start () {
        while(true) 
        {
            yield return new WaitForSeconds(Random.Range(.1f, 1f));
            iTween.RotateBy(gameObject, new Vector3(0, 0, -.1f), 1);
        }
    }

	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.tag == "Weapon" && Player.player_status == Player.Player_Status.Attack)
			Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Weapon" && Player.player_status == Player.Player_Status.Attack)
			Destroy(gameObject);
	}
}
