using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Minimap;


public class MinimapHandler : MonoBehaviour
{
    [SerializeField] private MinimapIcon playerIcon;
    public bool isExpand = false;
    public GameObject MinimapSmall;
    public GameObject MinimapExpand;
    
    public void ToggleMinimap()
    {
       isExpand = !isExpand;
        if(isExpand == false)
        {
            MinimapSmall.SetActive(true);
            MinimapExpand.SetActive(false);
            Minimap.MinimapController.ZoomIn();

        }
        else
        {

            MinimapSmall.SetActive(false);
            MinimapExpand.SetActive(true);
            Minimap.MinimapController.ZoomOut();


        }

       //MinimapWindow.Hide();
    }
   
}
