using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float height;
	public float width;

	public GameObject player;

	// Use this for initialization
	void Start () {

		Follow();
		CheckBoundary();
	}
	
	// Update is called once per frame
	void Update () {
		Follow();
		CheckBoundary();
	}

	void Follow(){
		transform.position = new Vector3(player.transform.position.x, 
		                                 player.transform.position.y, 
		                                 transform.position.z);
	}

	void CheckBoundary(){
		if(!IsWithinBounds()){
			MakeWithinBounds();
		}
	}

	bool IsWithinBounds(){
		
	}

	void MakeWithinBounds(){
	
	}
}
