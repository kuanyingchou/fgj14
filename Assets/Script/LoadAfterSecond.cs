using UnityEngine;
using System.Collections;

public class LoadAfterSecond : MonoBehaviour {
	public float waitTime = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitAndLoad(waitTime));
	}

	/*
	// Update is called once per frame
	void Update () {
	
	}
	*/

	IEnumerator WaitAndLoad(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
