using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap{
    public static class MinimapController
    {
        public static void ShowWindow()
        {
            MinimapWindow.Show();
        }
        public static void HideWindow()
        {
            MinimapWindow.Hide();
        }

        public static void SetZoom(float orthographicSize)
        {
            MinimapCamera.SetZoom(orthographicSize);
        }
        public static float GetZoom()
        {
            return MinimapCamera.GetZoom();
        }

        public static void ZoomIn()
        {
            MinimapCamera.ZoomIn();
        }
        public static void ZoomOut()
        {
            MinimapCamera.ZoomOut();
        }
    }
}

    

