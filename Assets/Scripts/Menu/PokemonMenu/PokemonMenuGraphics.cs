using UnityEngine;
using UnityEngine.UI;

public class PokemonMenuGraphics : MonoBehaviour
{
    [SerializeField] Texture2D icon_ball, icon_ball_selected;
    [SerializeField] Texture2D icon_item;
    [SerializeField] Texture2D overlay_hp_back, overlay_hp_back_faint, overlay_hp, overlay_hp_back_swap;
    [SerializeField] Texture2D panel_blank, panel_rect, panel_rect_faint, panel_rect_faint_sel, panel_rect_sel, panel_rect_swap, panel_round, panel_round_faint, panel_round_faint_sel, panel_round_sel;
    [SerializeField] Texture2D cancel, cancel_sel;
    [SerializeField] Texture2D panel_rect_swap_sel, panel_rect_swap_sel2, panel_round_swap_sel, panel_round_swap_sel2, panel_round_swap;
    [HideInInspector] public Sprite greenhp, yellowhp, redhp;
    PokemonMenu pm;


    public void Awake()
    {
        greenhp = Sprite.Create(overlay_hp, new Rect(0, overlay_hp.height * 2 / 3, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
        yellowhp = Sprite.Create(overlay_hp, new Rect(0, overlay_hp.height / 3, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
        redhp = Sprite.Create(overlay_hp, new Rect(0, 0, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
        pm = FindObjectOfType<PokemonMenu>();
    }

    public void selected(Image ri)
    {
        if (ri.sprite.texture == panel_rect)
            ri.sprite = Sprite.Create(panel_rect_sel, new Rect(0, 0, panel_rect_sel.width, panel_rect_sel.height), new Vector2(0, 0), .01f);
        else if (ri.sprite.texture == panel_rect_faint)
            ri.sprite = Sprite.Create(panel_rect_faint_sel, new Rect(0, 0, panel_rect_faint_sel.width, panel_rect_faint_sel.height), new Vector2(0, 0), .01f);
        else if (ri.sprite.texture == panel_round)
            ri.sprite = Sprite.Create(panel_round, new Rect(0, 0, panel_round.width, panel_round.height), new Vector2(0, 0), .01f);
        else if (ri.sprite.texture == panel_round_faint)
            ri.sprite = Sprite.Create(panel_round_faint, new Rect(0, 0, panel_round_faint.width, panel_round_faint.height), new Vector2(0, 0), .01f);
    }

    public void SetBG(Image ri, Image hp, int i, bool fainted)
    {
        if (fainted)
        {
            if (i == 0)
                ri.sprite = Sprite.Create(panel_round_faint, new Rect(0, 0, panel_round_faint.width, panel_round_faint.height), new Vector2(0, 0), .01f);
            else
                ri.sprite = Sprite.Create(panel_rect_faint, new Rect(0, 0, panel_rect_faint.width, panel_rect_faint.height), new Vector2(0, 0), .01f);
            hp.sprite = Sprite.Create(overlay_hp_back_faint, new Rect(0, 0, overlay_hp_back_faint.width, overlay_hp_back_faint.height), new Vector2(0, 0), .01f);
        }
        else
        {
            if (i == 0)
                ri.sprite = Sprite.Create(panel_round, new Rect(0, 0, panel_round.width, panel_round.height), new Vector2(0, 0), .01f);
            else
                ri.sprite = Sprite.Create(panel_rect, new Rect(0, 0, panel_rect.width, panel_rect.height), new Vector2(0, 0), .01f);
            hp.sprite = Sprite.Create(overlay_hp_back, new Rect(0, 0, overlay_hp_back.width, overlay_hp_back.height), new Vector2(0, 0), .01f);
        }
    }

    public void SetNull(Image ri)
    {
        ri.sprite = Sprite.Create(panel_blank, new Rect(0, 0, panel_blank.width, panel_blank.height), new Vector2(0, 0), 0.01f);
    }

    public void HighLight(Image ri, int i, bool fainted)
    {
        if (i == 6)
        {
            ri.sprite = Sprite.Create(cancel_sel, new Rect(0, 0, cancel.width, cancel.height), new Vector2(0, 0), 0.01f);
            return;
        }
        if (pm.swapping)
        {
            if (i == pm.swapsel)
            {
                if (i == 0)
                    ri.sprite = Sprite.Create(panel_round_swap_sel2, new Rect(0, 0, panel_round_swap.width, panel_round_swap.height), new Vector2(0, 0), 0.01f);
                else
                    ri.sprite = Sprite.Create(panel_rect_swap_sel2, new Rect(0, 0, panel_rect_swap.width, panel_rect_swap.height), new Vector2(0, 0), 0.01f);
            }
            else
            {
                if (i == 0)
                    ri.sprite = Sprite.Create(panel_round_swap_sel, new Rect(0, 0, panel_round.width, panel_round.height), new Vector2(0, 0), 0.01f);
                else
                    ri.sprite = Sprite.Create(panel_rect_swap_sel, new Rect(0, 0, panel_rect.width, panel_rect.height), new Vector2(0, 0), 0.01f);
            }
        }
        else
        {
            if (fainted)
            {
                if (i == 0)
                    ri.sprite = Sprite.Create(panel_round_faint_sel, new Rect(0, 0, panel_round_faint.width, panel_round_faint.height), new Vector2(0, 0), 0.01f);
                else
                    ri.sprite = Sprite.Create(panel_rect_faint_sel, new Rect(0, 0, panel_rect_faint.width, panel_rect_faint.height), new Vector2(0, 0), 0.01f);
            }
            else
            {
                if (i == 0)
                    ri.sprite = Sprite.Create(panel_round_sel, new Rect(0, 0, panel_round.width, panel_round.height), new Vector2(0, 0), 0.01f);
                else
                    ri.sprite = Sprite.Create(panel_rect_sel, new Rect(0, 0, panel_rect.width, panel_rect.height), new Vector2(0, 0), 0.01f);
            }
        }
    }

    public void UnHighlight(Image ri, int i, bool fainted)
    {
        if (i == 6)
        {
            ri.sprite = Sprite.Create(cancel, new Rect(0, 0, cancel.width, cancel.height), new Vector2(0, 0), 0.01f);
            return;
        }
        if (pm.swapping)
        {
            if (i == pm.swapsel)
            {
                if (i == 0)
                    ri.sprite = Sprite.Create(panel_round_swap, new Rect(0, 0, panel_round_swap.width, panel_round_swap.height), new Vector2(0, 0), 0.01f);
                else
                    ri.sprite = Sprite.Create(panel_rect_swap, new Rect(0, 0, panel_rect_swap.width, panel_rect_swap.height), new Vector2(0, 0), 0.01f);
            }
            else
            {
                if(fainted)
                {
                    if (i == 0)
                        ri.sprite = Sprite.Create(panel_round_faint, new Rect(0, 0, panel_round.width, panel_round.height), new Vector2(0, 0), 0.01f);
                    else
                        ri.sprite = Sprite.Create(panel_rect_faint, new Rect(0, 0, panel_rect.width, panel_rect.height), new Vector2(0, 0), 0.01f);
                }
                else
                {
                    if (i == 0)
                        ri.sprite = Sprite.Create(panel_round, new Rect(0, 0, panel_round.width, panel_round.height), new Vector2(0, 0), 0.01f);
                    else
                        ri.sprite = Sprite.Create(panel_rect, new Rect(0, 0, panel_rect.width, panel_rect.height), new Vector2(0, 0), 0.01f);
                }
            }
        }
        else
        {
            if (fainted)
            {
                if (i == 0)
                    ri.sprite = Sprite.Create(panel_round_faint, new Rect(0, 0, panel_round_faint.width, panel_round_faint.height), new Vector2(0, 0), 0.01f);
                else
                    ri.sprite = Sprite.Create(panel_rect_faint, new Rect(0, 0, panel_rect_faint.width, panel_rect_faint.height), new Vector2(0, 0), 0.01f);
            }
            else
            {
                if (i == 0)
                    ri.sprite = Sprite.Create(panel_round, new Rect(0, 0, panel_round.width, panel_round.height), new Vector2(0, 0), 0.01f);
                else
                    ri.sprite = Sprite.Create(panel_rect, new Rect(0, 0, panel_rect.width, panel_rect.height), new Vector2(0, 0), 0.01f);
            }
        }
    }

    public void OpenBall(Image ri)
    {
        ri.sprite = Sprite.Create(icon_ball_selected, new Rect(0, 0, icon_ball_selected.width, icon_ball_selected.height), new Vector2(0, 0), 0.01f);
    }

    public void CloseBall(Image ri)
    {
        ri.sprite = Sprite.Create(icon_ball, new Rect(0, 0, icon_ball.width, icon_ball.height), new Vector2(0, 0), 0.01f);
    }

    public void SwapHighlight(Image panel, Image hpbar, int i)
    {
        if(i == 0)
            panel.sprite = Sprite.Create(panel_round_swap, new Rect(0, 0, panel_round_swap.width, panel_round_swap.height), new Vector2(0, 0), 0.01f);
        else
            panel.sprite = Sprite.Create(panel_rect_swap, new Rect(0, 0, panel_rect_swap.width, panel_rect_swap.height), new Vector2(0, 0), 0.01f);
        hpbar.sprite = Sprite.Create(overlay_hp_back_swap, new Rect(0, 0, overlay_hp_back_swap.width, overlay_hp_back_swap.height), new Vector2(0, 0), 0.01f);
    }
}
