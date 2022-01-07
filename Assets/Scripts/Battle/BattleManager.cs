using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum BattleType{Wild, Trainer};
public class BattleManager : MonoBehaviour
{
    public BattleType bt = BattleType.Wild;

    public Tuple<Pokemon, int> wildPokemon;
    public Tuple<Pokemon, int>[] trainerPokemon;
    
    Battle b;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += init;
    }

    void init(Scene next, LoadSceneMode ls)
    {
        if(next.name == "Battle")
        {
            Battle b = FindObjectOfType<Battle>();
            b = new WildBattle(wildPokemon.Item1, wildPokemon.Item2);
        }
    }
}
