using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	GameObject gm;

	void Awake(){
		gm = GameObject.FindGameObjectWithTag("GM");
		
		if(gm == null){

			Instantiate(Resources.Load("GM"), Vector3.zero, Quaternion.identity);
		}
	}

	void Start () {
	}
	
	void Update () {
	
	}
}
