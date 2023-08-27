using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBattle : Battle
{
    BattleGraphicsManager graphicsManager;

    public WildBattle(Pokemon p, int level)
    {
        opponent = new Battler(p, level);
    }

    public void SendPokemon(FieldPokemon f)
    {
        player = new Battler(f);
    }

    public override void startBattle()
    {
        graphicsManager = FindObjectOfType<BattleGraphicsManager>();
        StartCoroutine(graphicsManager.setWildPokemon(opponent));
    }

    IEnumerator sendPokemon()
    {
        yield return null;
    }
}
