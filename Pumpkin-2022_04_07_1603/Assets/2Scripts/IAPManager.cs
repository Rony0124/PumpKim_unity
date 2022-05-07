using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public const string ProductDiamond100 = "Diamond-100"; //comsumable
    public const string ProductDiamond550 = "Diamond-550"; //comsumable
    public const string ProductDiamond1500 = "Diamond-1500"; //comsumable
    public const string ProductSubscription = "premium_subscription"; //subscription

    private const string _iOS_Dia100Id = "com.studio.app.dia_100";//개발자센터에서 상품아이디를 사용
    private const string _android_Dia100Id = "com.nopg.dia_100";
    
    private const string _iOS_Dia550Id = "com.studio.app.dia_550";//개발자센터에서 상품아이디를 사용
    private const string _android_Dia550Id = "com.nopg.dia_550";

    private const string _iOS_Dia1500Id = "com.studio.app.dia_1500";//개발자센터에서 상품아이디를 사용
    private const string _android_Dia1500Id = "com.nopg.dia_1500";


    private const string _iOS_PremiumSubscription = "com.studio.app.sub";//개발자센터에서 상품아이디를 사용, 해당앱 상품페이지에서 직접 작성해야한다
    private const string _android_PremiumSubscription = "com.studio.app.sub";

    private static IAPManager instance;

    public static IAPManager Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = FindObjectOfType<IAPManager>();
            if (instance == null) instance = new GameObject("IAP Manager").AddComponent<IAPManager>();
            return instance;
        }
    }

    private IStoreController storeController; //구매과정 제어
    private IExtensionProvider storeExtensionProvider;// 여러 플랫폼을 위한 확장 처리
    public PlayerData playerData;
    public bool isInitialized => storeController != null && storeExtensionProvider != null;
   /* public bool isInitialized
    {
        get
        {
            if(storeController != null && storeExtensionProvider != null)
            {
                return true;
            }
            return false;
        }
    }*/
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        playerData = FindObjectOfType<PlayerData>();
        InitUnityIAP();
    }
    void InitUnityIAP() 
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(
                ProductDiamond100, ProductType.Consumable, new IDs() {
                    {_iOS_Dia100Id, AppleAppStore.Name },
                    {_android_Dia100Id, GooglePlay.Name }
                }
        );
        builder.AddProduct(
               ProductDiamond550, ProductType.Consumable, new IDs() {
                    {_iOS_Dia550Id, AppleAppStore.Name },
                    {_android_Dia550Id, GooglePlay.Name }
               }
       );
        builder.AddProduct(
               ProductDiamond1500, ProductType.Consumable, new IDs() {
                    {_iOS_Dia1500Id, AppleAppStore.Name },
                    {_android_Dia1500Id, GooglePlay.Name }
               }
       );
        builder.AddProduct(
               ProductSubscription, ProductType.Subscription, new IDs() {
                    {_iOS_PremiumSubscription, AppleAppStore.Name },
                    {_android_PremiumSubscription, GooglePlay.Name }
               }
       );

        UnityPurchasing.Initialize(this, builder);
    } 

    public void OnInitialized(IStoreController contoller, IExtensionProvider extensions)
    {
        Debug.Log("초기화 성공");
        storeController = contoller;
        storeExtensionProvider = extensions;

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"유니티 IAP초기화 실패{error}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"구매상품 id: {args.purchasedProduct.definition.id}");
        if (args.purchasedProduct.definition.id == ProductDiamond100)
        {
            PlayerData.Instance.currentDiamond += 100;
            //ResourceController.instance.updateUI = true;
            Debug.Log("다이아구매처리100");


        } else if (args.purchasedProduct.definition.id == ProductDiamond550) {
            PlayerData.Instance.currentDiamond = PlayerData.Instance.currentDiamond + 550;
            //ResourceController.instance.UpdateUI();
            Debug.Log(PlayerData.Instance.currentDiamond);
            Debug.Log("다이아구매처리550");
            
        }
        else if (args.purchasedProduct.definition.id == ProductDiamond1500)
        {
            PlayerData.Instance.currentDiamond += 1500;
            //ResourceController.instance.updateUI = true;
            Debug.Log("다이아구매처리1500");
        }
        else if (args.purchasedProduct.definition.id == ProductSubscription)
        {
            Debug.Log("구독처리");
        }
        ResourceController.instance.UpdatePurchase();
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning($"구매실패 - {product.definition.id},{reason} ");
    }

    public void Purchase(string productId)
    {
        if (!isInitialized) return;

        var product = storeController.products.WithID(productId);
        if(product != null && product.availableToPurchase)
        {
            Debug.Log($"구매시도 - {product.definition.id}");
            
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log($"구매시도 불가 - {productId}");
        }
    }
    public void RestorePurchase()
    {
        if (!isInitialized) return;
        if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("구매복구시도");
            var appleExt = storeExtensionProvider.GetExtension<IAppleExtensions>();
            appleExt.RestoreTransactions(
                result => Debug.Log($"구매복구시도 결과 - {result}")
                );

        }
    }

    public bool HadPurchased(string productId)
    {
        if (!isInitialized) return false;

        var product = storeController.products.WithID(productId);
        if(product != null)
        {
            return product.hasReceipt;
        }

        return false;
    }
}
