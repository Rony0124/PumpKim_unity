using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DescriptionBehaviour : MonoBehaviour
{
    
    public GameObject scrollbar;
    
   
   
    private void FixedUpdate()
    {
        if (scrollbar.GetComponent<Scrollbar>().value < 0)
        {
            scrollbar.GetComponent<Scrollbar>().value = 1;
        }
        else
        {
            scrollbar.GetComponent<Scrollbar>().value -= 0.005f;
        }
    }
}
