using UnityEngine;
using System.Collections;

public class Trail2D : MonoBehaviour {
	TrailRenderer trail;

	// Use this for initialization
	void Start () {
		trail = GetComponent<TrailRenderer>();
		trail.sortingLayerID = 2;
	}

}
