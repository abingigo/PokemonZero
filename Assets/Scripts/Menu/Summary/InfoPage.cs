using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dexNumber;
    [SerializeField] TextMeshProUGUI species;
    [SerializeField] Image singleType, dualType1, dualType2;
    [SerializeField] TextMeshProUGUI currEXP;
    [SerializeField] TextMeshProUGUI tonextLvl;
    [SerializeField] Slider Exp;
    PlayerProfile pp;
    GeneralSprites gs;
    PlayerMovement pm;
    int currpoke;

    private void Awake()
    {
        pp = FindObjectOfType<PlayerProfile>();
        gs = FindObjectOfType<GeneralSprites>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject g = GameObject.FindObjectOfType<Summary>().GetScreen(1);
            g.SetActive(true);
            g.GetComponent<MemoPage>().Setup(currpoke);
            gameObject.SetActive(false);
            pm.gameObject.GetComponent<AudioSource>().clip = FindObjectOfType<AudioClips>().summaryPage;
            pm.gameObject.GetComponent<AudioSource>().Play();
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currpoke != pp.party_count - 1)
            {
                currpoke++;
                Setup(currpoke);
                FindObjectOfType<Summary>().Setup(currpoke);
            }
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currpoke != 0)
            {
                currpoke--;
                Setup(currpoke);
                FindObjectOfType<Summary>().Setup(currpoke);
            }
        }
    }

    public void Setup(int poke)
    {
        FieldPokemon b = pp.party[poke];

        dexNumber.text = b.pokemon.number.ToString();
        species.text = b.pokemon.Name;

        if(b.pokemon.type2 != null)
        {
            dualType1.sprite = (Sprite)gs.GetType().GetField(b.pokemon.type1.Name.ToString().ToLower()).GetValue(gs);
            dualType2.sprite = (Sprite)gs.GetType().GetField(b.pokemon.type2.Name.ToString().ToLower()).GetValue(gs);
            dualType1.color = new UnityEngine.Color(dualType1.color.r, dualType1.color.g, dualType1.color.b, 1);
            dualType2.color = new UnityEngine.Color(dualType2.color.r, dualType2.color.g, dualType2.color.b, 1);
            singleType.color = new UnityEngine.Color(singleType.color.r, singleType.color.g, singleType.color.b, 0);
        }
        else
        {
            dualType1.color = new UnityEngine.Color(dualType1.color.r, dualType1.color.g, dualType1.color.b, 0);
            dualType2.color = new UnityEngine.Color(dualType2.color.r, dualType2.color.g, dualType2.color.b, 0);
            singleType.color = new UnityEngine.Color(singleType.color.r, singleType.color.g, singleType.color.b, 1);
            singleType.sprite = (Sprite) gs.GetType().GetField(b.pokemon.type1.Name.ToString().ToLower()).GetValue(gs);
        }

        currEXP.text = b.currEXP.ToString();
        tonextLvl.text = (b.pokemon.growthRate.GetExp(b.level + 1) - b.currEXP).ToString();

        Exp.minValue = (float)b.pokemon.growthRate.GetExp(b.level);
        Exp.maxValue = (float)b.pokemon.growthRate.GetExp(b.level + 1);
        Exp.value = b.currEXP;
        currpoke = poke;
    }
}
