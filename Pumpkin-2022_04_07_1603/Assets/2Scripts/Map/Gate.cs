using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class Gate : MonoBehaviour
{
    static Gate instance;
    public static Gate Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Gate>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("Gate");
                    instance = instanceContainer.AddComponent<Gate>();

                }
            }
            return instance;
        }

    }
    void Awake() { instance = this; }
    public Vector2 gateDir;
    // Start is called before the first frame update
    
    public Vector3 CalcGateDis(Vector3 gatePos)
    {
       gateDir = (transform.parent.position - gatePos).normalized;
       return gateDir;
    }

    
}
