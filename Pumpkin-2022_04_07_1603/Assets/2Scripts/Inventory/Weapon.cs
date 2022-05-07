using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int _weaponid;
    public Sprite _icon;
    public int _dmg;
    public float _critical;
    public float _atkSpd;
    public string _name;
    
    public string _func;
    public string _content;


    public Weapon(int id, string name, int dmg, float critical, float atkSpd,string func, string content)
    {
        _weaponid = id;
        _dmg = dmg;
        _critical = critical;
        _atkSpd = atkSpd;
        _name = name;
        
        _func = func;
        _content = content;
        _icon = Resources.Load("Item_Icons/" + _weaponid.ToString(), typeof(Sprite)) as Sprite;
    }

}
