using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoveDetailsPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] moves = new TextMeshProUGUI[4];
    [SerializeField] TextMeshProUGUI[] pps = new TextMeshProUGUI[4];
    [SerializeField] Image[] types = new Image[4];
    [SerializeField] Image pokemon, type0, type1, type2, category;
    [SerializeField] TextMeshProUGUI power, accuracy, description;
    [SerializeField] Image cursor;
    Coroutine vibe;
    int movelength, currpoke, currmove, swapmove;
    [SerializeField] Texture2D cursor_sprite;
    Sprite cursorred, cursorwhite;
    bool switching;

    public void Awake()
    {
        cursorred = Sprite.Create(cursor_sprite, new Rect(0, cursor_sprite.height * 1 / 2, cursor_sprite.width, cursor_sprite.height / 2), new Vector2(0, 0), .01f);
        cursorwhite = Sprite.Create(cursor_sprite, new Rect(0, 0, cursor_sprite.width, cursor_sprite.height / 2), new Vector2(0, 0), .01f);
    }

    public void Setup(int poke, int move = 0)
    {
        movelength = 0;
        FieldPokemon b = PlayerProfile.party[poke];
        Sprite s = (Sprite)Resources.Load($"Graphics/Pokemon/Icons/{b.pokemon.Name.ToUpper()}", typeof(Sprite));
        Sprite[] s1 = new Sprite[] { Sprite.Create(s.texture, new Rect(new Vector2(0, 0), new Vector2(64, 64)), new Vector2(0, 0)), Sprite.Create(s.texture, new Rect(new Vector2(64, 0), new Vector2(64, 64)), new Vector2(0, 0)) };
        vibe = StartCoroutine(Vibing(s1));
        for(int i = 0; i < 4; i++)
        {
            if(b.battlerMoves[i].moves == null)
                break;
            moves[i].gameObject.SetActive(true);
            moves[i].text = b.battlerMoves[i].moves.Name;
            types[i].sprite = (Sprite) typeof(GeneralSprites).GetField(b.battlerMoves[i].moves.type.Name.ToString().ToLower()).GetValue(null);
            pps[i].text = $"{b.battlerMoves[i].currPP}/{b.battlerMoves[i].maxPP}";
            movelength++;
        }

        if(b.pokemon.type2 == null)
        {
            type0.sprite = (Sprite) typeof(GeneralSprites).GetField(b.pokemon.type1.Name.ToString().ToLower()).GetValue(null);
            type1.color = new UnityEngine.Color(type1.color.r, type1.color.g, type1.color.b, 0);
            type2.color = new UnityEngine.Color(type2.color.r, type2.color.g, type2.color.b, 0);
            type0.color = new UnityEngine.Color(type0.color.r, type0.color.g, type0.color.b, 1);
        }
        else
        {
            type1.sprite = (Sprite) typeof(GeneralSprites).GetField(b.pokemon.type1.Name.ToString().ToLower()).GetValue(null);
            type2.sprite = (Sprite) typeof(GeneralSprites).GetField(b.pokemon.type2.Name.ToString().ToLower()).GetValue(null);
            type1.color = new UnityEngine.Color(type1.color.r, type1.color.g, type1.color.b, 1);
            type2.color = new UnityEngine.Color(type2.color.r, type2.color.g, type2.color.b, 1);
            type0.color = new UnityEngine.Color(type0.color.r, type0.color.g, type0.color.b, 0);
        }
        currpoke = poke;
        Setup_Move(move);
        currmove = move;
        switching = false;
        cursor.sprite = cursorred;
        swapmove = -1;
    }

    public void Setup_Move(int move)
    {
        FieldPokemon b = PlayerProfile.party[currpoke];
        category.sprite = (Sprite) typeof(GeneralSprites).GetField(b.battlerMoves[move].moves.damageCategory.ToString().ToLower()).GetValue(null);

        if(b.battlerMoves[move].moves.basePower != 0)
            power.text = b.battlerMoves[move].moves.basePower.ToString();
        else
            power.text = "---";
        
        if(b.battlerMoves[move].moves.accuracy != 0)
            accuracy.text = b.battlerMoves[move].moves.accuracy.ToString() + "%";
        else
            accuracy.text = "---";
        description.text = b.battlerMoves[move].moves.description;
    }

    IEnumerator Vibing(Sprite[] s1)
    {
        int i = 0;
        while(true)
        {
            pokemon.sprite = s1[i];
            if (i == 0) i = 1; else i = 0;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && currmove != movelength- 1)
        {
            cursor.gameObject.transform.position = new Vector3(cursor.gameObject.transform.position.x, cursor.gameObject.transform.position.y - 65.5f, cursor.gameObject.transform.position.z);
            currmove++;
            Setup_Move(currmove);
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && currmove != 0)
        {
            cursor.gameObject.transform.position = new Vector3(cursor.gameObject.transform.position.x, cursor.gameObject.transform.position.y + 65.5f, cursor.gameObject.transform.position.z);
            currmove--;
            Setup_Move(currmove);
        }

        if(!switching)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
                FindObjectOfType<MovesPage>().inDetails = false;
                FindObjectOfType<MovesPage>().Setup(currpoke);
            }
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                switching = true;
                Image i = moves[currmove].GetComponentsInChildren<Image>()[1];
                i.sprite = cursorwhite;
                i.color = new UnityEngine.Color(i.color.r, i.color.g, i.color.b, 1);
                swapmove = currmove;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                switching = false;
                Image i = moves[swapmove].GetComponentsInChildren<Image>()[1];
                i.color = new UnityEngine.Color(i.color.r, i.color.g, i.color.b, 0);
            }
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                BattlerMoves bm = PlayerProfile.party[currpoke].battlerMoves[currmove];
                PlayerProfile.party[currpoke].battlerMoves[currmove] = PlayerProfile.party[currpoke].battlerMoves[swapmove];
                PlayerProfile.party[currpoke].battlerMoves[swapmove] = bm;
                Image i = moves[swapmove].GetComponentsInChildren<Image>()[1];
                i.color = new UnityEngine.Color(i.color.r, i.color.g, i.color.b, 0);
                switching = false;
                Setup(currpoke, currmove);
            }
        }
    }
}
