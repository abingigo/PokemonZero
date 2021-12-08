using UnityEngine;
using TMPro;

public class MemoPage : MonoBehaviour
{
    PlayerProfile pp;
    GeneralSprites gs;
    PlayerMovement pm;
    [SerializeField] TextMeshProUGUI nature;
    [SerializeField] TextMeshProUGUI date;
    [SerializeField] TextMeshProUGUI routeCaught;
    [SerializeField] TextMeshProUGUI lvlmet;
    [SerializeField] TextMeshProUGUI flair;
    int currpoke;

    private void Awake()
    {
        pp = FindObjectOfType<PlayerProfile>();
        gs = FindObjectOfType<GeneralSprites>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    public void Setup(int poke)
    {
        FieldPokemon b = pp.party[poke];
        nature.text = b.nature.ToString() + " nature";
        date.text = "Met on " + b.caughtDate;
        routeCaught.text = "Caught on" + b.routeCaught;
        lvlmet.text = "Met at level " + b.levelcaught;
        var textFile = Resources.Load<TextAsset>("Misc/flairs");
        string[] x = textFile.text.Split(new char[] {'\n'});
        flair.text = x[Random.Range(0,x.Length)];
        currpoke = poke;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject g = GameObject.FindObjectOfType<Summary>().GetScreen(0);
            g.SetActive(true);
            g.GetComponent<InfoPage>().Setup(currpoke);
            gameObject.SetActive(false);
            pm.gameObject.GetComponent<AudioSource>().clip = FindObjectOfType<AudioClips>().summaryPage;
            pm.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
