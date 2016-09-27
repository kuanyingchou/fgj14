using UnityEngine;
using System.Collections;

public class KZCircularTexture : MonoBehaviour {
    public int radius = 128;
    public Color color = new Color(1, 1, 1, 1);

    void Start () {
        UpdateProperties();
    }
    public void UpdateProperties() {
        GetComponent<Renderer>().material.mainTexture = 
                KZTexture.GetCircle(radius, color).ToTexture2D();
        GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse"); 
        GetComponent<Renderer>().material.color = Color.white;
        //] TODO note that this shader has to be used in the scene
    }
}
