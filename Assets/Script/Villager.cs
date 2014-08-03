using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour {

	public Transform leftHand, rightHand;

	// Use this for initialization
	IEnumerator Start () {
		while(true){
			Jump();
			yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Jump(){
		rigidbody2D.AddForce(new Vector2(0, Random.Range(3.0f, 6.0f)), ForceMode2D.Impulse);
	}
}
