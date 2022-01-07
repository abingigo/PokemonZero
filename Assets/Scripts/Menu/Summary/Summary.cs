using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Summary : MonoBehaviour
{
    int currPokemon = 0;
    [SerializeField] GameObject[] screens = new GameObject[5];
    [SerializeField] Image pokeball;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] Image gender;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] Image pokemon;
    [SerializeField] Image itemPic;
    [SerializeField] TextMeshProUGUI itemName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !FindObjectOfType<MovesPage>().inDetails)
        {
            if(FindObjectOfType<PokemonMenu>())
                FindObjectOfType<PokemonMenu>().inPartyMenu = true;
            gameObject.SetActive(false);
        }
    }

    public void Setup(int poke)
    {
        FieldPokemon b = PlayerProfile.party[poke];
        Name.text = b.nickName;

        if (b.gender == Gender.Female)
        {
            gender.sprite = GeneralSprites.genderFemale;
            gender.color = new UnityEngine.Color(gender.color.r, gender.color.g, gender.color.b); 
        }
        else if(b.gender == Gender.Male)
        {
            gender.sprite = GeneralSprites.genderMale;
            gender.color = new UnityEngine.Color(gender.color.r, gender.color.g, gender.color.b);
        }
        else
            gender.color = new UnityEngine.Color(gender.color.r, gender.color.g, gender.color.b, 0);

        level.text = b.level.ToString();

        if (b.isShiny)
            pokemon.sprite = (Sprite)Resources.Load($"Graphics/Pokemon/Front_shiny/{b.pokemon.Name.ToUpper()}", typeof(Sprite));
        else
            pokemon.sprite = (Sprite)Resources.Load($"Graphics/Pokemon/Front/{b.pokemon.Name.ToUpper()}", typeof(Sprite));

        if(b.heldItem == null)
        {
            itemPic.color = new UnityEngine.Color(itemPic.color.r, itemPic.color.g, itemPic.color.b, 0);
            itemName.text = "None";
        }
        else
        {
            itemPic.sprite = (Sprite)Resources.Load($"Graphics/Items/{b.heldItem.Name.ToUpper().Replace(" ", "")}", typeof(Sprite));
            itemName.text = b.heldItem.Name;
        }
        currPokemon = poke;
    }

    public GameObject GetScreen(int s)
    {        
        return screens[s];
    }
}
