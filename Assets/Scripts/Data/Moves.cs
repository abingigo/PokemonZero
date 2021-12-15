using UnityEngine;

public enum DamageCategory{ Physical, Special, Status }
public enum Target { None, User, NearAlly, UserOrNearAlly, UserAndAllies, NearFoe, AllNearFoes, RandomNearFoe, Foe, AllFoes, NearOther, AllNearOthers, Other, AllBattlers, UserSide, FoeSide, BothSides}

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
}
