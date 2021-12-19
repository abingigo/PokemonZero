using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovesPage : MonoBehaviour
{
    GeneralSprites gs;
    PlayerMovement pm;
    [SerializeField] TextMeshProUGUI[] moves = new TextMeshProUGUI[4];
    [SerializeField] TextMeshProUGUI[] pps = new TextMeshProUGUI[4];
    [SerializeField] Image[] types = new Image[4];
    [SerializeField] GameObject movedetailspage;
    public bool inDetails;
    int currpoke;

    public void Awake()
    {
        gs = FindObjectOfType<GeneralSprites>();
        pm = FindObjectOfType<PlayerMovement>();
        inDetails = false;
    }

    void Update()
    {
        if(!inDetails)
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                movedetailspage.SetActive(true);
                movedetailspage.GetComponent<MoveDetailsPage>().Setup(currpoke);
                inDetails = true;
            }
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                GameObject g = GameObject.FindObjectOfType<Summary>().GetScreen(4);
                g.SetActive(true);
                g.GetComponent<RibbonsPage>().Setup(currpoke);
                gameObject.SetActive(false);
                pm.gameObject.GetComponent<AudioSource>().clip = AudioClips.summaryPage;
                pm.gameObject.GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GameObject g = GameObject.FindObjectOfType<Summary>().GetScreen(2);
                g.SetActive(true);
                g.GetComponent<SkillsPage>().Setup(currpoke);
                gameObject.SetActive(false);
                pm.gameObject.GetComponent<AudioSource>().clip = AudioClips.summaryPage;
                pm.gameObject.GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(currpoke != PlayerProfile.party_count - 1)
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

    public void Setup(int poke)
    {
        FieldPokemon b = PlayerProfile.party[poke];
        for(int i = 0; i < 4; i++)
            moves[i].gameObject.SetActive(false);
        
        for(int i = 0; i < 4; i++)
        {
            if(b.battlerMoves[i].moves == null)
                break;
            moves[i].gameObject.SetActive(true);
            moves[i].text = b.battlerMoves[i].moves.Name;
            types[i].sprite = (Sprite) gs.GetType().GetField(b.battlerMoves[i].moves.type.Name.ToString().ToLower()).GetValue(gs);
            pps[i].text = $"{b.battlerMoves[i].currPP}/{b.battlerMoves[i].maxPP}";
        }
        currpoke = poke;
    }
}
