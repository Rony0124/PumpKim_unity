using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public int _skillid;
    public Sprite _icon;
    public string _name;
    public int _level;
    public string _func;
    public string _content;
   

   
    public Skill(int id, string name, int level, string func, string content)
    {
        _skillid = id;
       
        _name = name;
        _level = level;
        _func = func;
        _content = content;
        _icon = Resources.Load("Item_Icons/" + _skillid.ToString(), typeof(Sprite)) as Sprite;
    }

}
