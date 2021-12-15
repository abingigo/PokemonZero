using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp, attack, defense, spatk, spdef, speed;
    [SerializeField] Slider hpslider;
    [SerializeField] TextMeshProUGUI ability, abilitydesc;
    Sprite redhp, yellowhp, greenhp;
    [SerializeField] Texture2D overlay_hp;
    PlayerProfile pp;
    PlayerMovement pm;
    int currpoke;

    public void Awake()
    {
        greenhp = Sprite.Create(overlay_hp, new Rect(0, overlay_hp.height * 2 / 3, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
        yellowhp = Sprite.Create(overlay_hp, new Rect(0, overlay_hp.height / 3, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
        redhp = Sprite.Create(overlay_hp, new Rect(0, 0, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
        pp = FindObjectOfType<PlayerProfile>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    public void Setup(int poke)
    {
        FieldPokemon b = pp.party[poke];
        hp.text = $"{b.currHP}/{b.maxHP}";
        attack.text = b.attack.ToString();
        defense.text = b.defense.ToString();
        spatk.text = b.specialAttack.ToString();
        spdef.text = b.specialDefense.ToString();
        speed.text = b.speed.ToString();
        if(b.currHP * 100 / b.maxHP < 33)
            hpslider.GetComponentInChildren<Image>().sprite = redhp;
        else if(b.currHP * 100 / b.maxHP < 50)
            hpslider.GetComponentInChildren<Image>().sprite = yellowhp;
        else
            hpslider.GetComponentInChildren<Image>().sprite = greenhp;
        hpslider.minValue = 0;
        hpslider.maxValue = b.maxHP;
        hpslider.value = b.currHP;
        ability.text = b.ability.Name;
        abilitydesc.text = b.ability.description;
        currpoke = poke;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            GameObject g = GameObject.FindObjectOfType<Summary>().GetScreen(3);
            g.SetActive(true);
            g.GetComponent<MovesPage>().Setup(currpoke);
            gameObject.SetActive(false);
            pm.gameObject.GetComponent<AudioSource>().clip = FindObjectOfType<AudioClips>().summaryPage;
            pm.gameObject.GetComponent<AudioSource>().Play();
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
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
}