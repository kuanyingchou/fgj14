using UnityEngine;
using System.Collections;

public class Particle2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerID = 2;
	}

}
