using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPurchase : MonoBehaviour
{
    public ResourceType resourceType;
    public int price;

    public void SetInfo()
    {
        PurchaseBtn.instance.resourceType = resourceType;
        PurchaseBtn.instance.price = price;
    }
}
