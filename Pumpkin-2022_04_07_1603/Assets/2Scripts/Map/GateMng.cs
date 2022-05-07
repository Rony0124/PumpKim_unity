using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMng : MonoBehaviour
{
    static GateMng instance;
    public static GateMng Instance
    {
        get
        {
            
            return instance;
        }

    }
    void Awake() { instance = this; }
    public GameObject[] openGate;
    public GameObject[] closeGate;
    // Start is called before the first frame update
   
}
