using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    public string targetProductId;
    
    public void HandleClick()
    {
        if(targetProductId == IAPManager.ProductSubscription)
        {
            if (IAPManager.Instance.HadPurchased(targetProductId))
            {
                Debug.Log("이미 구매완료한 상품");
            }
        }
        IAPManager.Instance.Purchase(targetProductId);
        PlayCloudDataManager.Instance.SaveData();
        //;
        //StartCoroutine(Purchaseing());
    }
   
}
