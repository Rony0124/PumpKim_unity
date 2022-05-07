using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    static SkillSlot instance;
    public static SkillSlot Instance
    {
        get { return instance; }
    }
    void Awake() { instance = this; }
    public Image icon;
    public Text skillName_Text;
    //public Text skillLevel_Text;
    ///public int slotNumber;

    public void AddSkill(Skill skill)
    {
        skillName_Text.text = skill._name;
        icon.sprite = skill._icon;
       
        //slotNumber++;
    }

    public void RemoveItem()
    {
        skillName_Text.text = "";
        icon.sprite = null;

    }
}
