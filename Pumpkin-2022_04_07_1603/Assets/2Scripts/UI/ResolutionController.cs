
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    public Vector2 standard_resolution;
    private Camera theCamera;
    public bool isLandScape;

    private float rate;
    // Start is called before the first frame update
    void Start()
    {
        rate = standard_resolution.y / standard_resolution.x;
        theCamera = CameraMovement.Instance.theCamera;
        
    }

    // Update is called once per frame
    void Update()
    {
        Setting();
    }

    private void Setting()
    {
        int size = Screen.height - Screen.width;
        bool isScreenWrong = (size > 0 && isLandScape) || (size < 0 && !isLandScape);
        if (isScreenWrong) return;
        float mobile_rate = (float)Screen.height / (float)Screen.width;

        //위아래가 길어지는경우
        if(mobile_rate > rate)
        {
            float h = rate / mobile_rate;
            theCamera.rect = new Rect(0, (1 - h) / 2, 1, h);
        }
        else
        {
            float w = mobile_rate / rate;
            theCamera.rect = new Rect((1 - w) / 2, 0, w, 1);
        }
        this.enabled = false;
    }
}
