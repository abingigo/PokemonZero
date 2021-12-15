using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] GameObject options, arrow;
    [SerializeField] GameObject summaryScreen;
    
    PokemonMenuGraphics pmg;
    PlayerProfile pp;

    [HideInInspector] public bool inPartyMenu = false, selectedPokemon = false, swapping = false;
    
    int currentSel = 0, j = 0;
    
    [HideInInspector] public int swapsel;
    
    Coroutine[] vibes = new Coroutine[6];

    void Update()
    {
        if(inPartyMenu || swapping)
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                UnHighlight(currentSel);
                CloseBall(currentSel);
                if (currentSel == 6)
                    currentSel--;
                else
                    currentSel -= 2;
                if (currentSel < 0)
                    currentSel = 5 + (currentSel + 1) % 2;
                while (currentSel > pp.party_count)
                    currentSel -= 2;
                Highlight(currentSel);
                OpenBall(currentSel);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                UnHighlight(currentSel);
                CloseBall(currentSel);
                currentSel += 2;
                if (currentSel == 7)
                    currentSel = 6;
                if (currentSel > 6)
                    currentSel = currentSel % 2;
                while (currentSel > pp.party_count)
                    currentSel += 2;
                Highlight(currentSel);
                OpenBall(currentSel);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                UnHighlight(currentSel);
                CloseBall(currentSel);
                currentSel++;
                if (currentSel > 5 || currentSel > pp.party_count)
                    currentSel = 0;
                Highlight(currentSel);
                OpenBall(currentSel);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                UnHighlight(currentSel);
                CloseBall(currentSel);
                currentSel--;
                if (currentSel < 0)
                    currentSel = 5;
                while (currentSel > pp.party_count)
                    currentSel --;
                Highlight(currentSel);
                OpenBall(currentSel);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
                Select(currentSel);
            if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
        else if(selectedPokemon)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && j > 0)
            {
                j--;
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y + 42, arrow.transform.localPosition.z);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && j < 3)
            {
                j++;
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y - 42, arrow.transform.localPosition.z);
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCursor;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
            {
                if(j == 1)
                {
                    swapping = true;
                    pmg.SwapHighlight(slots[currentSel].GetComponent<Image>(), healthBars[currentSel].GetComponentsInChildren<Image>()[2], currentSel);
                    swapsel = currentSel;
                }
                if(j == 0)
                {
                    summaryScreen.SetActive(true);
                    summaryScreen.GetComponent<Summary>().Setup(currentSel);
                    summaryScreen.GetComponent<Summary>().GetScreen(0).SetActive(true);
                    summaryScreen.GetComponent<Summary>().GetScreen(0).GetComponent<InfoPage>().Setup(currentSel);
                }
                else if(j == 3)
                {
                    inPartyMenu = true;
                    if (pp.party_count == 1)
                        text.text = "Choose Pokemon or cancel";
                    else
                        text.text = "Choose a Pokemon";
                }
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selDecision;
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
                selectedPokemon = false;
                options.SetActive(false);
                j = 0;
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, 47, arrow.transform.localPosition.z);
            }
        }
    }

    public void ShowMenu()
    {
        pp = FindObjectOfType<PlayerProfile>();
        pmg = FindObjectOfType<PokemonMenuGraphics>();
        text.text = "Choose Pokemon or cancel";

        for (int i = 0; i < pp.party_count; i++)
            Setup(i);

        for (int i = pp.party_count; i < 6; i++)
            pmg.SetNull(slots[i].GetComponent<Image>());
        Highlight(currentSel);
        OpenBall(currentSel);
        inPartyMenu = true;
    }

    void Setup(int i)
    {
        pmg.SetBG(slots[i].GetComponent<Image>(), healthBars[i].GetComponentsInChildren<Image>()[2], i, pp.party[i].currHP == 0);
        slots[i].transform.GetChild(0).gameObject.SetActive(true);
        Sprite s = (Sprite)Resources.Load($"Graphics/Pokemon/Icons/{pp.party[i].pokemon.Name.ToUpper()}", typeof(Sprite));
        Sprite[] s1 = new Sprite[] { Sprite.Create(s.texture, new Rect(new Vector2(0, 0), new Vector2(64, 64)), new Vector2(0, 0)), Sprite.Create(s.texture, new Rect(new Vector2(64, 0), new Vector2(64, 64)), new Vector2(0, 0)) };
        vibes[i] = StartCoroutine(Vibing(i, s1));
        level[i].text = pp.party[i].level.ToString();
        nickName[i].text = pp.party[i].nickName;
        health[i].text = $"{pp.party[i].currHP}/{pp.party[i].maxHP}";
        healthBars[i].maxValue = pp.party[i].maxHP;
        healthBars[i].value = pp.party[i].currHP;
        if (pp.party[i].currHP * 100 / pp.party[i].maxHP < 33)
            healthBars[i].GetComponentsInChildren<Image>()[1].sprite = pmg.redhp;
        else if (pp.party[i].currHP * 100 / pp.party[i].maxHP <= 50)
            healthBars[i].GetComponentsInChildren<Image>()[1].sprite = pmg.yellowhp;
        else
            healthBars[i].GetComponentsInChildren<Image>()[1].sprite = pmg.greenhp;
        if (pp.party[i].gender == Gender.Male)
            slots[i].GetComponentsInChildren<Image>()[7].sprite = FindObjectOfType<GeneralSprites>().genderMale;
        else if (pp.party[i].gender == Gender.Female)
            slots[i].GetComponentsInChildren<Image>()[7].sprite = FindObjectOfType<GeneralSprites>().genderFemale;
        else
            slots[i].GetComponentsInChildren<Image>()[7].enabled = false;
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
                pmg.HighLight(slots[i].GetComponent<Image>(), i, pp.party[i].currHP == 0);
            else
                pmg.HighLight(slots[i].GetComponent<Image>(), i, pp.party[i].currHP == 0);
        }
        else
            pmg.HighLight(slots[i].GetComponent<Image>(), i, pp.party[i].currHP == 0);
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
                pmg.UnHighlight(slots[i].GetComponent<Image>(), i, pp.party[i].currHP == 0);
            else
                pmg.UnHighlight(slots[i].GetComponent<Image>(), i, pp.party[i].currHP == 0);
        }
        else
            pmg.UnHighlight(slots[i].GetComponent<Image>(), i, pp.party[i].currHP == 0);
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
            text.text = "Do what with " + pp.party[currentSel].nickName;
            options.SetActive(true);
            inPartyMenu = false;
            selectedPokemon = true;
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selDecision;
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
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCancel;
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>().Play();
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    IEnumerator Swap(int i, int j)
    {
        float x = Mathf.Abs(slots[i].GetComponent<RectTransform>().localPosition.x), y = - Mathf.Abs(slots[i].GetComponent<RectTransform>().localPosition.x);
        while (true)
        {
            slots[i].GetComponent<RectTransform>().localPosition += new Vector3((2 * (i % 2) - 1) * 3f, 0, 0);
            slots[j].GetComponent<RectTransform>().localPosition += new Vector3((2 * (j % 2) - 1) * 3f, 0, 0);
            x -= 3f;
            yield return null;
            if (x <= y)
                break;
        }
        FieldPokemon b = pp.party[i];
        pp.party[i] = pp.party[j];
        pp.party[j] = b;

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

        swapping = false;
        selectedPokemon = false;
        inPartyMenu = true;
        currentSel = j;
        Highlight(currentSel);
        text.text = "Choose Pokemon or cancel";
    }
}
