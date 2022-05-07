using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public WeaponType Weapon;
    Image img;
    public List<WeaponSlot> Weapons = new List<WeaponSlot>();
    
    public void OnClick()
    {
        DataMng.instance.currentWeapon = Weapon;
        OnSelect();
        for (int i = 0; i < Weapons.Count; i++)
        {
            if (Weapons[i] != this) Weapons[i].OnDeSelect();
        }
    }
    void Start()
    {
        img = GetComponent<Image>();
        
        if (DataMng.instance.currentWeapon == Weapon) OnSelect();
        else OnDeSelect();
    }
    void OnSelect()
    {
        PlayerData.Instance.ApplyWeapon();
        img.color = new Color(1f, 1f, 1f, 0.5f);
    }

    void OnDeSelect()
    {
        
        img.color = new Color(1f, 1f, 1f, 1f);
    }
}
