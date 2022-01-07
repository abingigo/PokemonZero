using UnityEngine;
using UnityEngine.UI;

public class BattleGraphicsManager : MonoBehaviour
{
    [SerializeField] GameObject speechBox;
    [SerializeField] Image[] choices = new Image[4];
    [SerializeField] Image battleBG;

    void init()
    {
        choices[0].sprite = BattleGraphics.fight;
        choices[1].sprite = BattleGraphics.pokemon;
        choices[2].sprite = BattleGraphics.bag;
        choices[3].sprite = BattleGraphics.run;
    }
}
