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
		MakeWithinBounds();
	}

	float GetCameraWidth(){
		return camera.orthographicSize * 2 * Screen.width / Screen.height;
	}

	float GetCameraHeight(){
		return camera.orthographicSize * 2;
	}

	float GetLeftBound(){
		return -width / 2 + GetCameraWidth() * 0.5f;
	}

	float GetRightBound(){
		return width / 2 - GetCameraWidth() * 0.5f;
	}

	float GetLowerBound(){
		return -height / 2 + GetCameraHeight() * 0.5f;
	}

	float GetUpperBound(){
		return height / 2 - GetCameraHeight() * 0.5f;
	}

	void MakeWithinBounds(){
		Vector3 pos = transform.position;

		if(transform.position.x < GetLeftBound())
			transform.position = new Vector3(GetLeftBound(), pos.y, pos.z);
		else if(transform.position.x > GetRightBound())
			transform.position = new Vector3(GetRightBound(), pos.y, pos.z);

		if(transform.position.y < GetLowerBound())
			transform.position = new Vector3(pos.x, GetLowerBound(), pos.z);
		else if(transform.position.y > GetUpperBound())
			transform.position = new Vector3(pos.x, GetUpperBound(), pos.z);
	}
}
