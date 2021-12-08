using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp, attack, defense, spatk, spdef, speed;
    [SerializeField] Slider hpsilder;
    [SerializeField] TextMeshProUGUI ability, abilitydesc;
    Sprite redhp, yellowhp, greenhp;
    [SerializeField] Texture2D overlay_hp;

    public void Awake()
    {
        greenhp = Sprite.Create(overlay_hp, new Rect(0, overlay_hp.height * 2 / 3, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
        yellowhp = Sprite.Create(overlay_hp, new Rect(0, overlay_hp.height / 3, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
        redhp = Sprite.Create(overlay_hp, new Rect(0, 0, overlay_hp.width, overlay_hp.height / 3), new Vector2(0, 0), .01f);
    }

    public void Setup()
    {
        
    }
}