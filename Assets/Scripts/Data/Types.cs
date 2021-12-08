using UnityEngine;

public enum Type {NULL, Normal, Fire, Water, Grass, Bug, Electric, Rock, Ground, Steel, Ice, Psychic, Dark, Ghost, Fighting, Flying, Dragon, Poison, Fairy}
public class Types : ScriptableObject
{
    public Sprite picture;
    public Type Name;
    public Types[] weakness;
    public Types[] resistance;
    public Types[] immunity;

    public bool isImmuneToParalysis, isImmuneToBurn, isImmuneToPoisoning, isImmuneToFreezing, isPseudoType, isSpecialType;
}
