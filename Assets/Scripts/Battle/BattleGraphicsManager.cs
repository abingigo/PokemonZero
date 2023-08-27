using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;

public class BattleGraphicsManager : MonoBehaviour
{
    [SerializeField] GameObject speechBox;
    [SerializeField] Image[] choices = new Image[4];
    [SerializeField] Image battleBG;
    [SerializeField] Image playerPerson;
    [SerializeField] Image enemyPokemon;
    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] Image playerPokemon;
    public int x = 5;


    void Awake()
    {
        choices[0].sprite = BattleGraphics.fight;
        choices[1].sprite = BattleGraphics.pokemon;
        choices[2].sprite = BattleGraphics.bag;
        choices[3].sprite = BattleGraphics.run;

        if(PlayerProfile.gender == 0)
            StartCoroutine(TrainerGuy());
        else
            StartCoroutine(TrainerGal());
    }

    IEnumerator TrainerGuy()
    {
        Texture2D sprite = Resources.Load<Texture2D>("Graphics/Trainers/TrainerMale");
        Sprite[] positions = new Sprite[5];

        for(int i = 0; i < 5; i++)
            positions[i] = Sprite.Create(sprite, new Rect(sprite.width * i / 5, 0, sprite.width / 5, sprite.height), Vector2.zero, .01f);

        playerPerson.sprite = positions[0];
        yield return null;
    }

    IEnumerator TrainerGal()
    {
        Texture2D sprite = Resources.Load<Texture2D>("Graphics/Trainers/TrainerFemale");
        Sprite[] positions = new Sprite[5];

        for(int i = 0; i < 5; i++)
            positions[i] = Sprite.Create(sprite, new Rect(sprite.width * i / 5, 0, sprite.width / 5, sprite.height), Vector2.zero, .01f);

        playerPerson.sprite = positions[0];
        yield return null;
    }

    public IEnumerator setWildPokemon(Battler b)
    {   
        Texture2D sprite;
        if(!b.poke.isShiny)
        {
            if (b.poke.gender == Gender.Female && File.Exists($"Graphics/Pokemon/Front/{b.poke.pokemon.Name.ToUpper()}_female"))
                sprite = Resources.Load<Texture2D>($"Graphics/Pokemon/Front/{b.poke.pokemon.Name.ToUpper()}_female");
            else
                sprite = Resources.Load<Texture2D>($"Graphics/Pokemon/Front/{b.poke.pokemon.Name.ToUpper()}");
        }
        else
        {
            if (b.poke.gender == Gender.Female && File.Exists($"Graphics/Pokemon/Front_shiny/{b.poke.pokemon.Name.ToUpper()}_female"))
                sprite = Resources.Load<Texture2D>($"Graphics/Pokemon/Front_shiny/{b.poke.pokemon.Name.ToUpper()}_female");
            else
                sprite = Resources.Load<Texture2D>($"Graphics/Pokemon/Front_shiny/{b.poke.pokemon.Name.ToUpper()}");
        }
        enemyPokemon.sprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), Vector2.zero, .01f);
        enemyName.text = b.poke.pokemon.Name;
        yield return null;
    }
}
