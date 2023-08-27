using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Encounters : MonoBehaviour, Encounter
{
    [System.Serializable] 
    struct dictionary
    {
        public Pokemon poke;
        public float encounterRate;
        public int minLevel;
        public int maxLevel;
    };

    [SerializeField] int encounterThreshold;
    [SerializeField] dictionary[] pokemonPresent;

    BattleManager bm;

    private void Awake()
    {
        float sum = 0;
        foreach(dictionary d in pokemonPresent)
            sum += d.encounterRate;
        if(sum != 100f)
            Debug.LogWarning("Encounters in a area do not equal 100");
        bm = FindObjectOfType<BattleManager>();
    }

    int GetRandomWeightedIndex(float[] weights)
    {
        float weightSum = 100;
        int index = 0;
        int lastIndex = weights.Length - 1;
        while (index < lastIndex)
        {
            if (Random.Range(0, weightSum) < weights[index])
                return index;
            weightSum -= weights[index++];
        }
        return index;
    }

    float[] makeArray(dictionary[] ar)
    {
        float[] array = new float[ar.Length];
        for(int i = 0; i < ar.Length; i++)
            array[i] = ar[i].encounterRate;
        return array;
    }

    public void encounter()
    {
        System.Random rand = new System.Random();
        if(rand.Next(0, 2879) >= encounterThreshold)
            return;

        float encounter = rand.Next(1, 100);
        int i = GetRandomWeightedIndex(makeArray(pokemonPresent));
        //float sum = pokemonPresent[0].encounterRate;
        
        //while(sum < encounter)
        //    sum += pokemonPresent[i++].encounterRate;

        int level = rand.Next(pokemonPresent[i].minLevel, pokemonPresent[i].maxLevel);

        bm.SetWild(pokemonPresent[i].poke, level);

        FindObjectOfType<StartGame>().gameObject.GetComponentInChildren<AudioSource>().Stop();
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().clip = AudioClips.wildBattleBGM;
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();

        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle()
    {
        FindObjectOfType<PlayerMovement>().enabled = false;
        FindObjectOfType<Menu>().enabled = false;
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<Animator>().SetFloat("speed", 0);
        yield return StartCoroutine(FindObjectOfType<BattleTransitions>().SquareSnakes());
        SceneManager.LoadScene("Battle");
    }
}
