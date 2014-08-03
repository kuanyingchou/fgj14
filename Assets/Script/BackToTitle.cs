using UnityEngine;
using System.Collections;

public class BackToTitle : MonoBehaviour {
    public void Update()
    {
        if (Input.GetKeyDown ("space"))
        {
            Application.LoadLevel("Title");
        }
    }
}
