using UnityEngine;

public enum locations{field, cave, surf, trainerfield, gymtrainer, gymleader, elitefour, champion};

public class Battle : MonoBehaviour
{
    public Battler opponent;
    public Battler player;
    locations location;

    public virtual void startBattle()
    {

    }
}
