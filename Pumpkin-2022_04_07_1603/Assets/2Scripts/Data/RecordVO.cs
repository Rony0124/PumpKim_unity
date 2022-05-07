using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RecordVO
{
    public int bestStage;
    public bool isClear;
 
    public RecordVO(int _bestStage, bool _isClear)
    {
        bestStage = _bestStage;
        isClear = _isClear;
    }
}
