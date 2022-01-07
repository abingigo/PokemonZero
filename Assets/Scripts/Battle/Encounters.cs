using UnityEngine;
using UnityEngine.SceneManagement;

public class Encounters : MonoBehaviour, Encounter
{
    [System.Serializable] 
    struct dictionary
    {
        public Pokemon poke;
        public float enounterRate;
        public int minLevel;
        public int maxLevel;
    };

    [SerializeField] int encounterThreshold;
    [SerializeField] dictionary[] pokemonPresent;

    private void Awake()
    {
        float sum = 0;
        foreach(dictionary d in pokemonPresent)
            sum += d.enounterRate;
        if(sum != 100f)
            Debug.LogWarning("Encounters in a area do not equal 100");
    }

    public void encounter()
    {
        System.Random rand = new System.Random();
        if(rand.Next(0, 2879) >= encounterThreshold)
            return;

        float encounter = rand.Next(1, 100);
        int i = 0;
        float sum = pokemonPresent[0].enounterRate;
        
        while(sum < encounter)
            sum += pokemonPresent[++i].enounterRate;

        int level = rand.Next(pokemonPresent[i].minLevel, pokemonPresent[i].maxLevel);
        
        Debug.Log(pokemonPresent[i].poke.Name);

        SceneManager.LoadScene("Battle");
    }
}
