using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct moves
{
    public int level;
    public Moves move;
}

public enum Methods 
{ Level, AttackGreater, DefenseGreater, AtkDefEqual, LevelFemale, LevelMale, Ninjask, Shedinja, LevelDay, LevelNight, Silcoon, Cascoon, //Level as parameter
  Happiness, HappinessDay, HappinessNight, HappinessMoveType,                                            //Happiness as parameter
  DayHoldItem, NightHoldItem, Item, ItemFemale, ItemMale,                             //Item as paramter
  Beauty,                                                                             //Beauty as parameter
  HasMove,                                                                            //Move as parameter
  HasInParty,                                                                         //Pokemon as parameter
  Location}                                                                           //Location as parameter

[System.Serializable]
public struct EvolveMethods
{
    public Pokemon product;
    public Methods method;
    public int level;
    public Items item;
    public int beauty;
    public Moves move;
    public Pokemon pokemon;
    public int location;
    public Types type;
}

public enum Shape { Head, Serpentine, Finned, HeadArms, HeadBase, BipedalTail, HeadLegs, Quadruped, Winged, Multiped, MultiBody, Bipedal, MultiWinged, Insectoid}

public enum GenderRate { AlwaysMale, FemaleOneEighth, Female25Percent, Female50Percent, Female75Percent, FemaleSevenEighths, AlwaysFemale, Genderless }

public enum GrowthRate { Fast, Medium, Slow, Parabolic, Erratic, Fluctuating}

static class GrowthRateMethods
{
    public static double GetExp(this GrowthRate gr, int level)
    {
        switch(gr)
        {
            case GrowthRate.Fast: return 4 * level * level * level / 5;

            case GrowthRate.Medium: return level * level * level;

            case GrowthRate.Slow: return 5 * level * level * level / 4;

            case GrowthRate.Parabolic: return 6 * level * level * level / 5 - 15 * level * level + 100 * level - 140;

            case GrowthRate.Erratic: if (level < 50) return level * level * level * (100 - level) / 50;
                                     else if (level < 68) return level * level * level * (150 - level) / 100;
                                     else if(level < 98) return level * level * level * (1911 - 10 * level) / 1500;
                                     else if(level < 100) return level * level * level * (160 - level) / 100;
                                     else return 0;
            case GrowthRate.Fluctuating: if(level < 15) return level * level * level * ((level + 1)/3 + 24) / 50;
                                         else if(level < 36) return level * level * level * (level + 14) / 50;
                                         else if(level < 100) return level * level * level * (Mathf.FloorToInt(level / 2) + 32) / 50;
                                         else return 0;
            default: return 0;
        }
    }
}

public enum Compatibility { Monster, Water1, Bug, Flying, Field, Fairy, Grass, Humanlike, Water3, Mineral, Amorphous, Water2, Ditto, Dragon, Undiscovered}

public enum Color { Black, Blue, Brown, Gray, Green, Pink, Purple, Red, White, Yellow}

public enum Habitat { None, Cave, Forest, Grassland, Mountain, Rare, RoughTerrain, Sea, Urban, WatersEdge}


public class Pokemon : ScriptableObject
{
    public int number;
    public string Name;
    public Types type1, type2, type3;
    public int[] baseStats = new int[6];
    public GenderRate genderRate;
    public GrowthRate growthRate;
    public int baseExp;
    public int[] effortPoints = new int[6];
    public int rareness;
    public int happiness;
    public List<Abilities> abilities;
    public Abilities hiddenAbility;
    public List<moves> moveset;
    public List<Moves> tutorMoves;
    public List<Moves> eggMoves;
    public List<Compatibility> compatibility;
    public int stepsToHatch;
    public float height;
    public float weight;
    public Color color;
    public Shape shape;
    public Habitat habitat;
    public string kind;
    public string pokedex;
    public int generation;
    public int battlerPlayerX;
    public int battlerPlayerY;
    public int battlerEnemyX;
    public int battlerEnemyY;
    public int battlerShadowX;
    public int battlerShadowSize;
    public List<EvolveMethods> evolveMethods;
}
