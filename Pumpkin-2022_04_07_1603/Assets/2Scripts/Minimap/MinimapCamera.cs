using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
    public class MinimapCamera : MonoBehaviour
    {
        private static MinimapCamera instance;

        private const float ZOOM_CHANGE_AMOUNT = 10f;
        private const float ZOOM_MIN = 30f;
        private const float ZOOM_MAX = 100f;
        private Camera minimapCamera;
        private float zoom;
        private void Awake()
        {
            instance = this;
            minimapCamera = transform.GetComponent<Camera>();
            zoom = minimapCamera.orthographicSize;
        }

        //static is to call this func using class reference
        public static void SetZoom(float orthographicSize)
        {
           instance.minimapCamera.orthographicSize = orthographicSize;
        }

        public static float GetZoom()
        {
            return instance.minimapCamera.orthographicSize;
        }

        public static void ZoomIn()
        {
            instance.zoom = 3.2f;
           
            Debug.Log(instance.zoom);
            SetZoom(instance.zoom);
        }
        public static void ZoomOut()
        {
            instance.zoom = 30f;
            
            SetZoom(instance.zoom);
        }
    }

}
