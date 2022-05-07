using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataVO 
{
    //public string playerId;
    public int hp;
    public float dmg;
    public float critical;
    public float atkSpd;
    public List<int> ownedItems = new List<int>();
    public List<RecordVO> records = new List<RecordVO>();
    public int Gold;
    public int Diamond;
    public int bestStage;

    public DataVO()
    {
       
    }
   
    public DataVO(PlayerData playerData)
    {
        //playerId = playerData.playerId;
        hp = playerData.maxHp;
        dmg = playerData.dmg;
        critical = playerData.critical;
        atkSpd = playerData.atkSpd;
        ownedItems = playerData.ownedItems;
        Gold = playerData.currentGold;
        Diamond = playerData.currentDiamond;
        records = playerData.records;
    }

}
