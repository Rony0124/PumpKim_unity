using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using System;

public static class CloudButtton
{
    
    public static DataVO LoadDiamond()
    {
        DataVO data = new DataVO();
        //PlayCloudDataManager.Instance.LoadFromCloud(PlayCloudDataManager.Instance.m_saveFileName, (string datatoLoad) => { data = JsonUtility.FromJson<DataVO>(datatoLoad); });
        ResourceController.instance.UpdateUI();
        Debug.Log(JsonUtility.ToJson(data));
        return data;
    }

    public static void SaveDiamond(PlayerData playerdata)
    {
        string jsonString = JsonUtility.ToJson(playerdata);
        //특정한 문자를 로드하고 세이브할 수 있다.
        //PlayCloudDataManager.Instance.SaveToCloud(jsonString, PlayCloudDataManager.Instance.m_saveFileName);

    }
}
