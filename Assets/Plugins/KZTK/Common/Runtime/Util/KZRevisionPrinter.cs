using UnityEngine;
using System.Collections;
using System;

//Attach it to any gameobject in scene to print revision number

//2013.3.15  ken  initial version
public class KZRevisionPrinter : MonoBehaviour {
    private KZRevision revision;
    //private Rect labelRect=new Rect(780, 770, 500, 30);
    private static int fontSize = 24;
    private static Rect labelRect;
    private static Rect shadowRect;
    private static Color fontColor = Color.white;

    public void Awake() {
        if(!Debug.isDebugBuild) {
            Destroy(this);
        }

        labelRect=new Rect(
                10, 10, Screen.width - 20, fontSize*2);
        shadowRect=new Rect(
            labelRect.x + 1, labelRect.y + 1, 
            labelRect.width, labelRect.height);

        revision=KZRevision.Load();
    }

    public void OnGUI() {
        //#if UNITY_ANDROID && !UNITY_EDITOR

        if(revision == null) {
            //Write("Version N/A. Kizi Lab Inc.");
        } else {
            Write(revision.ToHTML());
        }
        //#endif
    }

    private void Write(string text) {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        labelStyle.fontSize = fontSize;

        //back
        GUI.contentColor = Color.black;
        GUI.Label(shadowRect, text, labelStyle);

        //front
        GUI.contentColor = fontColor;
        GUI.Label(labelRect, text, labelStyle);
    }
}
