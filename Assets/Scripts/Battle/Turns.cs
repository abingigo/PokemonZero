using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather{Normal, Rain, HeavyRain, Sun, HarshSun, Hail, Sandstorm, StrongWind};
public enum Terrain{Grassy, Misty, Psychic, Electric};

public class Turns
{
    public int turnCount = 0;

    public Moves playerMove;
    public Moves enemyMove;

    public Weather weather;
    public Terrain terrain;
}
