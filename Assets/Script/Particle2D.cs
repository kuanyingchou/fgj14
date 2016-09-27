using UnityEngine;
using System.Collections;

public class Particle2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerID = 2;
	}

}
