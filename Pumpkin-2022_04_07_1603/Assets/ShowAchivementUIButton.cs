using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAchivementUIButton : MonoBehaviour
{
    public void ShowUI()
    {
        GooglePlayServiceManager.Instance.ShowAchivementUI();
    }
}
