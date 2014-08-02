using UnityEngine;
using System.Collections;

[System.Serializable]
public class KZSize{
    public float width;
    public float height;
    public static KZSize zero = new KZSize( 0, 0 );

    public KZSize(float w, float h){
        width = w;
        height = h;
    }

    public KZSize(KZSize that) {
        width = that.width;
        height = that.height;
    }

    public static bool operator ==(KZSize c1, KZSize c2){
        return c1.width == c2.width && c1.height == c2.height; 
    }

    public static bool operator !=(KZSize c1, KZSize c2){
        return !( c1 == c2 ) ; 
    }
} 
