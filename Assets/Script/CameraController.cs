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
		DelayedFollow(1);
	}

	void DelayedFollow(float coef){
		Vector3 dest = new Vector3(player.transform.position.x, 
		                           player.transform.position.y, 
		                           transform.position.z);
		transform.position = (dest - transform.position) * coef + transform.position; 
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
			pos = new Vector3(GetLeftBound(), pos.y, pos.z);
		else if(transform.position.x > GetRightBound())
			pos = new Vector3(GetRightBound(), pos.y, pos.z);

		if(transform.position.y < GetLowerBound())
			pos = new Vector3(pos.x, GetLowerBound(), pos.z);
		else if(transform.position.y > GetUpperBound())
			pos = new Vector3(pos.x, GetUpperBound(), pos.z);

		transform.position = pos;
	}
}
