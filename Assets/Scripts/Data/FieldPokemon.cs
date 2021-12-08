using System;

public enum Gender { Male, Female, Genderless }

public enum Nature { Hardy, Lonely, Brave, Adamant, Naughty, Bold, Docile, Relaxed, Impish, Lax, Timid, Hasty, Serious, Jolly, Naive, Modest, Mild, Quiet, Bashful, Rash, Calm, Gentle, Sassy, Careful, Quirky}

public enum Status { Healthy, Sleep, Poison, Burn, Paralysis, Frozen}

[Serializable]
public class FieldPokemon
{
    public uint personalId;
    public string nickName;
    public Pokemon pokemon;
    public Abilities ability;
    public int level;
    public int currEXP;
    public int currHP;
    public int maxHP;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public BattlerMoves[] battlerMoves = new BattlerMoves[4];
    public Items heldItem;
    public int happiness;
    public Gender gender;
    public Nature nature;
    public bool isShiny;
    public int form;
    public Status status;
    public int statusCount;
    public int eggSteps;
    public int ballUsed;
    public int[] ev = new int[6];
    public int[] iv = new int[6];
    public int pokerus;
    public Pokemon fused;
    public int levelcaught;
    public String caughtDate;
    public String routeCaught;

    public FieldPokemon(Pokemon p, string nickname, int abilityFlag, int genderFlag, int natureFlag, int shinyFlag, int lvl, bool isEgg)
    {
        Random rand = new Random();
        personalId = (uint)rand.Next(-int.MaxValue, int.MaxValue);

        level = lvl;

        if (nickname != null)
            nickName = nickname;
        else
            nickName = p.Name;

        pokemon = p;

        switch(abilityFlag)
        {
            case -1:if(pokemon.abilities.Count == 2)
                    {
                        if(personalId / 65536 % 2 == 0)
                            ability = pokemon.abilities[0];
                        else
                            ability = pokemon.abilities[1];
                    }
                    else
                        ability = pokemon.abilities[0];
                    break;

            case 0:ability = pokemon.abilities[0];
                    break;
            
            case 1:ability = pokemon.abilities[1];
                    break;
            
            case 2:ability = pokemon.hiddenAbility;
                    break;
        }

        if (pokemon.genderRate == GenderRate.AlwaysFemale)
            gender = Gender.Female;
        else if (pokemon.genderRate == GenderRate.AlwaysMale)
            gender = Gender.Male;
        else if (pokemon.genderRate == GenderRate.Genderless)
            gender = Gender.Genderless;
        else
        {
            switch(genderFlag)
            {
                case -1:int pg = (int)personalId % 256;
                        int genderThreshold = 0;
                        switch(pokemon.genderRate)
                        {
                            case GenderRate.FemaleOneEighth:genderThreshold = 31;
                            break;

                            case GenderRate.Female25Percent:genderThreshold = 63;
                            break;

                            case GenderRate.Female50Percent:genderThreshold = 127;
                            break;

                            case GenderRate.Female75Percent:genderThreshold = 191;
                            break;

                            case GenderRate.FemaleSevenEighths:genderThreshold = 225;
                                break;
                        }
                        if (pg >= genderThreshold)
                            gender = Gender.Male;
                        else
                            gender = Gender.Female;
                        break;
                
                case 0: gender = Gender.Male;
                    break;
                
                case 1: gender = Gender.Female;
                    break;
            }
        }

        if (natureFlag == -1)
        {
            int c = (int)personalId % 25;
            nature = (Nature)c;
        }
        else
            nature = (Nature)natureFlag;

        if (shinyFlag == -1)
        {
            if ((personalId / 65536 | personalId % 65536) < 16)
                isShiny = true;
            else
                isShiny = false;
        }
        else if (shinyFlag == 0)
            isShiny = true;
        else
            isShiny = false;

        for (int i = 0; i < 6; i++)
            ev[i] = 0;

        for (int i = 0; i < 6; i++)
            iv[i] = rand.Next(0, 31);

        happiness = p.happiness;

        status = (Status)0;

        calcStats();

        currHP = maxHP;

        form = 0;

        fused = null;

        pokerus = 0;

        if (isEgg)
            eggSteps = p.stepsToHatch;
        else
            eggSteps = 0;

        statusCount = 0;

        ballUsed = 0;

        int j = 0;
        foreach(moves m in p.moveset)
        {
            if (m.level > level)
                break;
            else
                battlerMoves[j] = new BattlerMoves(m.move);
            j++;
            if (j == 4)
                j -= 4;
        }

        switch (nature)
        {
            case Nature.Adamant:
                attack *= (int)1.1;
                specialAttack *= (int)0.9;
                break;

            case Nature.Bold:
                defense *= (int)1.1;
                attack *= (int)0.9;
                break;

            case Nature.Brave:
                attack *= (int)1.1;
                speed *= (int)0.9;
                break;

            case Nature.Calm:
                specialDefense *= (int)1.1;
                attack *= (int)0.9;
                break;

            case Nature.Careful:
                specialDefense *= (int)1.1;
                specialAttack *= (int)0.9;
                break;

            case Nature.Gentle:
                specialDefense *= (int)1.1;
                defense *= (int)0.9;
                break;

            case Nature.Hasty:
                speed *= (int)1.1;
                defense *= (int)0.9;
                break;

            case Nature.Impish:
                defense *= (int)1.1;
                specialAttack *= (int)0.9;
                break;

            case Nature.Jolly:
                speed *= (int)1.1;
                specialAttack *= (int)0.9;
                break;

            case Nature.Lax:
                defense *= (int)1.1;
                specialDefense *= (int)0.9;
                break;

            case Nature.Lonely:
                attack *= (int)1.1;
                defense *= (int)0.9;
                break;

            case Nature.Mild:
                specialAttack *= (int)1.1;
                defense *= (int)0.9;
                break;

            case Nature.Modest:
                specialAttack *= (int)1.1;
                attack *= (int)0.9;
                break;

            case Nature.Naive:
                speed *= (int)1.1;
                specialDefense *= (int)0.9;
                break;

            case Nature.Naughty:
                attack *= (int)1.1;
                specialDefense *= (int)0.9;
                break;

            case Nature.Quiet:
                specialAttack *= (int)1.1;
                speed *= (int)0.9;
                break;

            case Nature.Rash:
                specialAttack *= (int)1.1;
                specialDefense *= (int)0.9;
                break;

            case Nature.Relaxed:
                defense *= (int)1.1;
                speed *= (int)0.9;
                break;

            case Nature.Sassy:
                specialDefense *= (int)1.1;
                speed *= (int)0.9;
                break;

            case Nature.Timid:
                speed *= (int)1.1;
                attack *= (int)0.9;
                break;
        }
    }

    public void heal()
    {
        currHP = maxHP;
        status = (Status)0;
        foreach(BattlerMoves m in battlerMoves)
            m.currPP = m.maxPP;
    }

    public void calcStats()
    {
        maxHP = (2 * pokemon.baseStats[0] + iv[0] + ev[0] / 4) * level / 100 + level + 10;
        attack = (2 * pokemon.baseStats[1] + iv[1] + ev[1] / 4) * level / 100 + 5;
        defense = (2 * pokemon.baseStats[2] + iv[2] + ev[2] / 4) * level / 100 + 5;
        speed = (2 * pokemon.baseStats[3] + iv[3] + ev[3] / 4) * level / 100 + 5;
        specialAttack = (2 * pokemon.baseStats[4] + iv[4] + ev[4] / 4) * level / 100 + 5;
        specialDefense = (2 * pokemon.baseStats[5] + iv[5] + ev[5] / 4) * level / 100 + 5;
    }
}
