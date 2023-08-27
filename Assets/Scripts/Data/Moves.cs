using UnityEngine;
using System;

public enum DamageCategory{ Physical, Special, Status }
public enum Target { None, User, NearAlly, UserOrNearAlly, UserAndAllies, NearFoe, AllNearFoes, RandomNearFoe, Foe, AllFoes, NearOther, AllNearOthers, Other, AllBattlers, UserSide, FoeSide, BothSides}

public enum HMMoves { Cut = 387, Fly = 212, Surf = 655, Strength = 368, Flash = 450, Dig= 301, Teleport = 601, SoftBoiled = 500, Whirlpool = 670, Waterfall = 660, RockSmash = 158,
                      Headbutt = 375, SweetScent = 508, MilkDrink = 471, Dive = 657, SecretPower = 377, Defog = 226, RockClimb = 359, Chatter = 217 };

public class Moves : ScriptableObject
{
    public int idNumber;
    public string Name;
    public string functionCode;
    public int basePower;
    public Types type;
    public DamageCategory damageCategory;
    public int accuracy;
    public int totalPP;
    public int additionalEffectChance;
    public Target target;
    public int priority;
    public string flags;
    public string description;

    Moves()
    {
        
    }
}
