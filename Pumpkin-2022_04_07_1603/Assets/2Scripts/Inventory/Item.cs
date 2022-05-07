using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{
    public int itemID; //아이템의 고유 id 값(중복불가)
    public string itemName; //아이템 이름(중복가능)
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType
    {   //소모품, 장비, 퀘스트, 잡템
        Use,
        Equip,
        Quest,
        ETC

    }
    //생성자로 클래스가 불려올때 값을 채워줄것
    public Item(int _itemID, string _itemName, string _itemDescription, ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDescription;
        itemType = _itemType;
        itemCount = _itemCount;
        //아이템이미지를 아이템 번호와 다르게 설정했다면 string으로 패러미터로 값을 받아와야한다
        //as 를 쓰지않으면 가져오갰다고 선언만 한꼴 (캐스팅을 하여 실제로 변환 시켜야한다)
        itemIcon = Resources.Load("Item_Icons/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }
    
}
