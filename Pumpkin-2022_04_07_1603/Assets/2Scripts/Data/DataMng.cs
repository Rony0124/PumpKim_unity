using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Player, Player1
}
public enum SkillType
{
    None, DoubleShot, MultiShot, Richochet, HeadShot
}
public enum WeaponType
{
    Basic, GrapeCandy, CoffeeCandy, PumpkinCandy, StrongCandy, Fread, AChoco, WholeCake, Berrys_hot, SStar, Mr_Octo, IceCream
}

public class DataMng : MonoBehaviour
{

    public static DataMng instance;

    void Awake() 
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }
    [Header("Player")]
    public Character currentCharacter;
    public SkillType currentSkill;
    public WeaponType currentWeapon;
}
