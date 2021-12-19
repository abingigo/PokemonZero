[System.Serializable]
public class BattlerMoves
{
    public Moves moves;
    public int currPP;
    public int maxPP;
    public int ppup;

    public BattlerMoves(Moves move)
    {
        moves = move;
        if(move == null)
        {
            currPP = maxPP = 0;
            ppup = 0;
            return;
        }
        currPP = maxPP = move.totalPP;
        ppup = 0;
    }

    public bool Ppup_used()
    {
        if (ppup == 3)
            return false;
        else
        {
            ppup++;
            maxPP += maxPP / 5;
            return true;
        }
    }
}
