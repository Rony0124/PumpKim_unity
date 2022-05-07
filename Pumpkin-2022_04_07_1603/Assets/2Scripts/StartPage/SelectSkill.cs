using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    public SkillType skill;
    Image img;
    public List<SelectSkill> skills= new List<SelectSkill>();
    [SerializeField] public AudioClip buttonCliclSFX;

    void Start()
    {
        img = GetComponent<Image>();
        if (DataMng.instance.currentSkill == skill) OnSelect();
       // else OnDeSelect(0);
    }
    public void Click()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        DataMng.instance.currentSkill = skill;
        
        OnSelect();
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i] != this) skills[i].OnDeSelect(i);
        }
    }
    void OnSelect()
    {
        PlayerData.Instance.SkillSelected();
        img.color = new Color(1f, 1f, 1f, 0.5f);
    }

    void OnDeSelect(int i)
    {
        PlayerData.Instance.SkillDeselected(i);
        img.color = new Color(1f, 1f, 1f, 1f);
    }
}
