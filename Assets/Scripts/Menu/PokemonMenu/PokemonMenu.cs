using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Add field moves when selecting pokemon in Select()

public class PokemonMenu : MonoBehaviour
{
    [SerializeField] GameObject[] slots = new GameObject[6];
    [SerializeField] Image[] pokeballs = new Image[6];
    [SerializeField] Image[] pokemon = new Image[6];
    [SerializeField] TextMeshProUGUI[] level = new TextMeshProUGUI[6];
    [SerializeField] TextMeshProUGUI[] nickName = new TextMeshProUGUI[6];
    [SerializeField] TextMeshProUGUI[] health = new TextMeshProUGUI[6];
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject cancel;
    [SerializeField] Slider[] healthBars = new Slider[6];
    [SerializeField] GameObject summaryScreen;

    OptionsBox ob;

    PokemonMenuGraphics pmg;

    [HideInInspector] public bool inPartyMenu = false, swapping = false;
    
    int currentSel = 0;
    
    [HideInInspector] public int swapsel;
    
    Coroutine[] vibes = new Coroutine[6];

    void Update()
    {
        if(inPartyMenu || swapping)
        {
            if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && currentSel > 1)
            {
                UnHighlight(currentSel);
                CloseBall(currentSel);
                currentSel -= 2;
                if(currentSel > PlayerProfile.party_count)
                    currentSel = PlayerProfile.party_count - 1;
                Highlight(currentSel);
                OpenBall(currentSel);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentSel <= PlayerProfile.party_count - 1)
            {
                UnHighlight(currentSel);
                CloseBall(currentSel);
                currentSel += 2;
                if(currentSel >= PlayerProfile.party_count)
                    currentSel = 6;
                Highlight(currentSel);
                OpenBall(currentSel);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentSel <= PlayerProfile.party_count)
            {
                UnHighlight(currentSel);
                CloseBall(currentSel);
                currentSel++;
                if(currentSel >= PlayerProfile.party_count)
                    currentSel = 6;
                Highlight(currentSel);
                OpenBall(currentSel);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentSel > 0)
            {
                UnHighlight(currentSel);
                CloseBall(currentSel);
                currentSel--;
                if(currentSel > PlayerProfile.party_count)
                    currentSel = PlayerProfile.party_count - 1;
                Highlight(currentSel);
                OpenBall(currentSel);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
                Select(currentSel);
            if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
    }

    public void ShowMenu()
    {
        pmg = FindObjectOfType<PokemonMenuGraphics>();
        text.text = "Choose Pokemon or cancel";

        for (int i = 0; i < PlayerProfile.party_count; i++)
            Setup(i);

        for (int i = PlayerProfile.party_count; i < 6; i++)
            pmg.SetNull(slots[i].GetComponent<Image>());
        Highlight(currentSel);
        OpenBall(currentSel);
        inPartyMenu = true;
    }

    void Setup(int i)
    {
        pmg.SetBG(slots[i].GetComponent<Image>(), healthBars[i].GetComponentsInChildren<Image>()[2], i, PlayerProfile.party[i].currHP == 0);
        slots[i].transform.GetChild(0).gameObject.SetActive(true);
        Sprite s = (Sprite)Resources.Load($"Graphics/Pokemon/Icons/{PlayerProfile.party[i].pokemon.Name.ToUpper()}", typeof(Sprite));
        Sprite[] s1 = new Sprite[] { Sprite.Create(s.texture, new Rect(new Vector2(0, 0), new Vector2(64, 64)), new Vector2(0, 0)), Sprite.Create(s.texture, new Rect(new Vector2(64, 0), new Vector2(64, 64)), new Vector2(0, 0)) };
        vibes[i] = StartCoroutine(Vibing(i, s1));
        level[i].text = PlayerProfile.party[i].level.ToString();
        nickName[i].text = PlayerProfile.party[i].nickName;
        health[i].text = $"{PlayerProfile.party[i].currHP}/{PlayerProfile.party[i].maxHP}";
        healthBars[i].maxValue = PlayerProfile.party[i].maxHP;
        healthBars[i].value = PlayerProfile.party[i].currHP;
        if (PlayerProfile.party[i].currHP * 100 / PlayerProfile.party[i].maxHP < 33)
            healthBars[i].GetComponentsInChildren<Image>()[1].sprite = pmg.redhp;
        else if (PlayerProfile.party[i].currHP * 100 / PlayerProfile.party[i].maxHP <= 50)
            healthBars[i].GetComponentsInChildren<Image>()[1].sprite = pmg.yellowhp;
        else
            healthBars[i].GetComponentsInChildren<Image>()[1].sprite = pmg.greenhp;
        if (PlayerProfile.party[i].gender == Gender.Male)
            slots[i].GetComponentsInChildren<Image>()[7].sprite = FindObjectOfType<GeneralSprites>().genderMale;
        else if (PlayerProfile.party[i].gender == Gender.Female)
            slots[i].GetComponentsInChildren<Image>()[7].sprite = FindObjectOfType<GeneralSprites>().genderFemale;
        else
            slots[i].GetComponentsInChildren<Image>()[7].enabled = false;
    }

    void Select(int i)
    {
        if (i == 6)
            Cancel();
        else
        {
            if(swapping)
            {
                StartCoroutine(Swap(swapsel, i));
                return;
            }
            text.text = "Do what with " + PlayerProfile.party[currentSel].nickName;
            ob = OptionsBox.Instance;
            ob.addOptions(new string[]{"Summary", "Switch"});

            /*for(int j = 0; j < 4; j++)
            {
                Moves m = pp.party[currentSel].battlerMoves[j].moves;
                if(m == null)
                    break;
                if(m.)
            }*/ //We add field moves here

            ob.addOptions(new string[] {"Cancel"});
            ob.p = positions.choice;
            ob.ShowOptions(transform, recieveChoice);

            inPartyMenu = false;
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.selDecision;
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void Cancel()
    {
        if(swapping)
        {
            swapping = false;
            inPartyMenu = true;
            Highlight(swapsel);
            UnHighlight(6);
            currentSel = swapsel;
            return;
        }
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.selCancel;
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
        StopAllCoroutines();
        gameObject.SetActive(false);
        FindObjectOfType<Menu>().ShowMenu();
    }

    IEnumerator Swap(int i, int j)
    {
        if(i != j)
        {
            float x = Mathf.Abs(slots[i].GetComponent<RectTransform>().localPosition.x), y = - Mathf.Abs(slots[i].GetComponent<RectTransform>().localPosition.x);
            
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.partySwitch;
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();

            while (true)
            {
                slots[i].GetComponent<RectTransform>().localPosition += new Vector3((2 * (i % 2) - 1) * 3f, 0, 0);
                slots[j].GetComponent<RectTransform>().localPosition += new Vector3((2 * (j % 2) - 1) * 3f, 0, 0);
                x -= 3f;
                yield return null;
                if (x <= y)
                    break;
            }
            FieldPokemon b = PlayerProfile.party[i];
            PlayerProfile.party[i] = PlayerProfile.party[j];
            PlayerProfile.party[j] = b;
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.selCancel;
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();

            StopCoroutine(vibes[i]);
            StopCoroutine(vibes[j]);
            Setup(i);
            Setup(j);

            while(true)
            {
                slots[i].GetComponent<RectTransform>().localPosition -= new Vector3((2 * (i % 2) - 1) * 3f, 0, 0);
                slots[j].GetComponent<RectTransform>().localPosition -= new Vector3((2 * (j % 2) - 1) * 3f, 0, 0);
                x += 3f;
                yield return null;
                if (x >= -y)
                    break;
            }
        }

        swapping = false;
        inPartyMenu = true;
        currentSel = j;
        Highlight(currentSel);
        text.text = "Choose Pokemon or cancel";
    }

    public void recieveChoice(string s)
    {
        switch (s)
        {
            case "Summary": summaryScreen.SetActive(true);
                            summaryScreen.GetComponent<Summary>().Setup(currentSel);
                            summaryScreen.GetComponent<Summary>().GetScreen(0).SetActive(true);
                            summaryScreen.GetComponent<Summary>().GetScreen(0).GetComponent<InfoPage>().Setup(currentSel);
                            break;

            case "Switch":  swapping = true;
                            pmg.SwapHighlight(slots[currentSel].GetComponent<Image>(), healthBars[currentSel].GetComponentsInChildren<Image>()[2], currentSel);
                            swapsel = currentSel;
                            break;

            case "Cancel":  if (PlayerProfile.party_count == 1)
                                text.text = "Choose Pokemon or cancel";
                            else
                                text.text = "Choose a Pokemon";
                            inPartyMenu = true;
                            break;
        }
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = AudioClips.selDecision;
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
    }

    IEnumerator Vibing(int a, Sprite[] s1)
    {
        int i = 0;
        while(true)
        {
            pokemon[a].GetComponent<Image>().sprite = s1[i];
            if (i == 0) i = 1; else i = 0;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Highlight(int i)
    {
        if (i == 6)
        {
            pmg.HighLight(cancel.GetComponent<Image>(), i, false);
            return;
        }
        if (swapping)
        {
            if (i == swapsel)
                pmg.HighLight(slots[i].GetComponent<Image>(), i, PlayerProfile.party[i].currHP == 0);
            else
                pmg.HighLight(slots[i].GetComponent<Image>(), i, PlayerProfile.party[i].currHP == 0);
        }
        else
            pmg.HighLight(slots[i].GetComponent<Image>(), i, PlayerProfile.party[i].currHP == 0);
    }

    void UnHighlight(int i)
    {
        if (i == 6)
        {
            pmg.UnHighlight(cancel.GetComponent<Image>(), i, false);
            return;
        }
        if (swapping)
        {
            if(i == swapsel)
                pmg.UnHighlight(slots[i].GetComponent<Image>(), i, PlayerProfile.party[i].currHP == 0);
            else
                pmg.UnHighlight(slots[i].GetComponent<Image>(), i, PlayerProfile.party[i].currHP == 0);
        }
        else
            pmg.UnHighlight(slots[i].GetComponent<Image>(), i, PlayerProfile.party[i].currHP == 0);
    }

    void OpenBall(int i)
    {
        if (i == 6)
            return;
        pmg.OpenBall(pokeballs[i]);
    }

    void CloseBall(int i)
    {
        if (i == 6)
            return;
        pmg.CloseBall(pokeballs[i]);
    }
}
