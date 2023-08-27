using UnityEngine;

public enum locations{field, cave, surf, trainerfield, gymtrainer, gymleader, elitefour, champion};

public class Battle: MonoBehaviour
{
    public Battler player;
    public Battler opponent;
    public locations location;
    
    public virtual void startBattle()
    {

    }
}
