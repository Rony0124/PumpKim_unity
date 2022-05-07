using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DatabaseMng : MonoBehaviour
{
    static public DatabaseMng instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            //instance = this;
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public string[] var_name;
    public float[] var;
    public GameObject key;
    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();
    public List<Skill> skillList = new List<Skill>();
    public List<Weapon> weaponList = new List<Weapon>();
    public void UseItem(int _itemID)
    {
        PlayerData playerData = PlayerData.Instance;
        
        switch (_itemID)
        {
            case 10001:
                Debug.Log(_itemID);
                if (playerData.currentHp == playerData.maxHp)
                {
                    playerData.maxHp += 1;
                    playerData.currentHp += 1;
                    break;
                }
                else
                {
                    playerData.currentHp += 1;
                    break;
                }
                    
                
            case 10002:
                Debug.Log("열쇠");
                playerData.ItemList.Add(_itemID);
                UIController.Instance.ItemPIckUp(_itemID);
                break;
            case 10003:
                int rand = Random.Range(3, 30);
                playerData.currentGold += rand;
                UIController.Instance.GainGold(rand);
                break;
            case 20001:
                Debug.Log("장비장착");
                playerData.dmg += 300;
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001, "heart", "체력 +1", Item.ItemType.Use));
        itemList.Add(new Item(10002, "key", "열쇠", Item.ItemType.Use));
        itemList.Add(new Item(10003, "Gold", "Gold", Item.ItemType.Use));
        skillList.Add(new Skill(0, "none", 1, "basic", "aa"));
        skillList.Add(new Skill(1, "Double Shot", 1, "shoots the bullet twice in a row", "Damage + 10"));
        skillList.Add(new Skill(2, "Multi Shot", 1, "shoots multiple bullets", "Ciritical + 3%"));
        skillList.Add(new Skill(3, "Ricochet", 1, "targets the next closest enemy", "Damage + 10"));
        skillList.Add(new Skill(4, "Head shot", 1, "gives a chance to kill the enemy in an instant", "Ciritical + 3%"));
        skillList.Add(new Skill(5, "Atk Increase", 1, "Damage increases by 20", "aa"));
        skillList.Add(new Skill(6, "AtkSpd Increase", 1, "Attack Speed increases by +25%", "aa"));
        skillList.Add(new Skill(7, "Hp Increase", 1, "Maximum and current Health increases by 1", "aa"));
        skillList.Add(new Skill(8, "Critical Increase", 1, "Critical rate increases by +5%", "aa"));
        skillList.Add(new Skill(9, "Atk Increase", 1, "Damage increases by 35", "aa"));
        weaponList.Add(new Weapon(1000, "Basic", 0,0,0, "", "AA"));
        weaponList.Add(new Weapon(1001, "GrapeCandy", 20,0,0 ,"Dmg + 20", "AA"));
        weaponList.Add(new Weapon(1002, "CoffeeCandy", 40, 0.05f, 0, "Dmg + 40, \nCri + 5%", "AA"));
        weaponList.Add(new Weapon(1003, "PumpkinCandy", 30, 0.03f, 0.1f, "Dmg + 40, \nCri + 5%, \nAtkSpd + 10%", "AA"));
        weaponList.Add(new Weapon(1004, "StrongCandy", 100, 0.08f, 0.15f, "Dmg + 100, \nCri + 8%, \nAtkSpd + 15%", "AA"));
        weaponList.Add(new Weapon(1005, "Fread", 10, 0.05f, 0.20f, "Dmg + 10, \nCri + 5%, \nAtkSpd + 20%", "AA"));
        weaponList.Add(new Weapon(1006, "AChoco", 15, 0.04f, 0.15f, "Dmg + 15, \nCri + 4%, \nAtkSpd + 15%", "AA"));
        weaponList.Add(new Weapon(1007, "WholeCake", 20, 0.03f, 0.10f, "Dmg + 20, \nCri + 3%, \nAtkSpd + 10%", "AA"));
        weaponList.Add(new Weapon(1008, "Berrys_hot", 55, 0.3f, 0.5f, "Dmg + 55, \nCri + 30%, \nAtkSpd + 50%", "AA"));
        weaponList.Add(new Weapon(1009, "SStar", 2800, 0.2f, 0.5f, "Dmg + 400, \nCri + 20%, \nAtkSpd + 50%", "AA"));
        weaponList.Add(new Weapon(1010, "Mr_Octo", 400, 0.3f, 0.8f, "Dmg + 60, \nCri + 30%, \nAtkSpd + 80%", "AA"));
        weaponList.Add(new Weapon(1011, "IceCream", 70, 0.08f, 0.9f, "Dmg + 70, \nCri + 8%, \nAtkSpd + 90%", "AA"));

    }

}
