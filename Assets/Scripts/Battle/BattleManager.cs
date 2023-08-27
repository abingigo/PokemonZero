using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum BattleType{Wild, Trainer};

public class BattleManager : MonoBehaviour
{
    public BattleType bt = BattleType.Wild;

    public Tuple<Pokemon, int> wildPokemon;
    public Tuple<Pokemon, int>[] trainerPokemon;
    public Battle battle;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += init;
    }

    public void SetWild(Pokemon p, int level)
    {
        wildPokemon = new Tuple<Pokemon, int>(p, level);
    }

    void init(Scene next, LoadSceneMode ls)
    {
        if(next.name == "Battle")
        {
            if(bt == BattleType.Wild)
            {
                battle = gameObject.AddComponent(typeof(WildBattle)) as WildBattle;
                battle.opponent = new Battler(wildPokemon.Item1, wildPokemon.Item2);
                battle.startBattle();
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= init;
    }
}
