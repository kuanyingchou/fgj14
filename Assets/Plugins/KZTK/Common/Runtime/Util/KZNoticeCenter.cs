using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
 
//2013.3.29  ken  modified from 
//                http://wiki.unity3d.com/index.php/NotificationCenter3_5

public class KZNoticeCenter {
 
    private static KZNoticeCenter instance;

    public static bool DEBUG=false;
 
    public static KZNoticeCenter Instance {
        get {
            if (instance == null) {
                instance = new KZNoticeCenter();
            }
            return instance;
        }
    }

    private Dictionary<string, List<System.Object>> registrations = 
       new Dictionary<string, List<System.Object>>();

    public void RemoveAll() {
        registrations.Clear();
    }
 
 
    // since the == operator on System.Object doesn't handle Unity's 
    // Destroy(), we use UnityEngine.Object as observer type.
    public void AddObserver(System.Object observer, string name) {
        if(name == null || name == "") {
            Debug.LogError ("Please specify a name for this observer!");
            return;
        }
        if(registrations.ContainsKey(name) == false) {
            registrations[name] = new List<System.Object>();
        }
 
        List<System.Object> notifyList = registrations[name];
 
        if(notifyList.Contains(observer)) {
            Debug.LogWarning("Observer had been added before!");
        } else {
            notifyList.Add(observer);
        }
 
    }
 
    public void RemoveObserver(System.Object observer, string name) {
        if(!registrations.ContainsKey(name)) return;

        List<System.Object> notifyList = registrations[name];
 
        if(notifyList.Contains(observer)) {
            notifyList.Remove(observer);
        }
        if(notifyList.Count == 0) {
            registrations.Remove(name);
        }
    }

    public List<System.Object> GetObservers(string name) {
        if(string.IsNullOrEmpty(name) || 
           !registrations.ContainsKey(name) ||
           registrations[name] == null) 
            return new List<System.Object>();
        return registrations[name];
    }
 
    public void PostNotice (System.Object aSender, string aName) {
        PostNotice(aSender, aName, null);
    }
 
    public void PostNotice(System.Object aSender, string aName, Hashtable aData)
    {
        PostNotice(new KZNotice(aSender, aName, aData));
    }
    public void PostNotice(System.Object aSender, string aName, System.Object aData)
    {
        PostNotice(new KZNotice(aSender, aName, aData));
    }
 
    private void PostNotice(KZNotice aNotice)
    {
        if(aNotice.name == null || aNotice.name == "")
        {
            Debug.Log("Null name sent to PostNotice.");
            return;
        }

        List<System.Object> notifyList = null;
        if(registrations.ContainsKey(aNotice.name)) {
            notifyList = registrations[aNotice.name];
        }
 
        if(notifyList == null)
        {
            Debug.Log("No observer found for notice \""+aNotice.name+"\"");
            return;
        }
 
        List<System.Object> observersToRemove = new List<System.Object>();
        List<System.Object> receiver = new List<System.Object>();

        for(int i=0; i<notifyList.Count; i++)
        {
            System.Object observer=notifyList[i];
            if(observer == null || IsDestroyed(observer)) { 
                observersToRemove.Add(observer);
                //since the observer may be destroyed after subscription
            } else {
                if(DEBUG) receiver.Add(observer);
                KZUtil.Call(observer, aNotice.name, aNotice);
                //Since the target type of SendMessage() is GameObject,
                //when multiple scripts attached to the same GameObject
                //while listening to the same event, the event would be 
                //sent to the same GameObject several times and produce 
                //undesirable results. In order to fix the problem, we 
                //bypass GameObject, and use reflection to send the event 
                //directly to the observing Component.
            }
        }
        if(DEBUG) {
            string list = KZUtil.Join(receiver, 
                    o => o.GetType() + 
                    ((o.GetType() == typeof(MonoBehaviour))
                    ?
                    "(" + ((MonoBehaviour)o).GetInstanceID() +
                    ") of \"" + ((MonoBehaviour)o).gameObject.name + "\""
                    :
                    "")
                    , 
                    ", ");
            string msg="Sent \""+aNotice.name+"\" to ";
            Debug.Log(msg + list);
        }
 
        foreach(System.Object observer in observersToRemove) {
            notifyList.Remove(observer);
        }
    }

    private bool IsDestroyed(System.Object o) {
        return (o is UnityEngine.Object && ((UnityEngine.Object)o) == null)?
                true:false; //note the cast is important in Unity 3 and 4
    }

}
 
public class KZNotice {
 
    public System.Object sender;
    public string name;
    public System.Object data;
 
    public KZNotice(System.Object aSender, string aName ) { 
        sender = aSender; 
        name = aName; 
    }
 
    public KZNotice(System.Object aSender, string aName, Hashtable aData) { 
        sender = aSender; 
        name = aName; 
        data = aData; 
    }

    public KZNotice(System.Object aSender, string aName, System.Object aData) { 
        sender = aSender; 
        name = aName; 
        data = aData; 
    }
}
