
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem 
{
   
    public static void Save(DataVO data)
    {
        Debug.Log("로컬 세이브 다이아: " + data.Diamond +
            "로컬 세이브 골드: " + data.Gold +
            "로컬 세이브 아이템: " + data.ownedItems +
            "로컬 세이브 데미지: " + data.dmg +
            "로컬 세이브 HP: " + data.hp +
            "로컬 세이브 크리티컬: " + data.critical +
            "로컬 세이브 공속: " + data.atkSpd +
            "로컬 세이브 점수: " + data.records);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "playerData9.bin";

        FileStream stream = new FileStream(path, FileMode.Create);
        //Debug.Log(data.records[0].bestStage);
        for(int i = 0; i< data.ownedItems.Count; i++)
        {
            Debug.Log(data.ownedItems[i]);
        }
        
        formatter.Serialize(stream, data);
        stream.Close();
        //PlayerData.data를 지워서 로드 과정이 필요 없을듯
    }

    public static DataVO Load()
    {
        string path = Application.persistentDataPath + "playerData9.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DataVO data = formatter.Deserialize(stream) as DataVO;
            stream.Close();

            Debug.Log("로컬 로드 다이아: " + data.Diamond +
                "로컬 로드 골드: " + data.Gold +
                "로컬 로드 아이템: " + data.ownedItems +
                "로컬 로드 데미지: " + data.dmg +
                "로컬 로드 HP: " + data.hp +
                "로컬 로드 크리티컬: " + data.critical +
                "로컬 로드 공속: " + data.atkSpd +
                "로컬 로드 점수: " + data.records);

            return data;

        }
        else
        {
            return null;
        }

    }
}
