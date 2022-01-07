public class WildBattle : Battle
{
    public WildBattle(Pokemon p, int level)
    {
        opponent = new Battler(p, level);
    }

    public void SendPokemon(FieldPokemon f)
    {
        player = new Battler(f);
    }

    public override void startBattle()
    {

    }
}
