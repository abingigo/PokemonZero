using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActions
{
    public LinkedList<BattleActions> currentActions;
    public bool allowActions = false;

    public void pushToHead(BattleActions ba)
    {
        currentActions.AddFirst(ba);
    }

    public void pushToTail(BattleActions ba)
    {
        currentActions.AddLast(ba);
    }

    public virtual void doActions()
    {
        if(!allowActions)
            return;
        foreach(BattleActions ba in currentActions)
            ba.doActions();
        allowActions = false;
    }
}

public class JustDisplay : BattleActions
{
    public override void doActions()
    {
        
    }
}