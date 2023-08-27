public class Battler
{
    public FieldPokemon poke;
    Abilities ability;
    BattlerMoves[] moves = new BattlerMoves[4];
    bool abilitySuppressed = false;

    int attack, defense, spatk, spdef, speed;
    float atkmult, defmult, spatkmult, spdefmult, speedmult;

    public Battler(Pokemon p, int level)
    {
        poke = new FieldPokemon(p, level);
        ability = poke.ability;
        moves = poke.battlerMoves;
    }

    public Battler(FieldPokemon f)
    {
        poke = f;
        ability = poke.ability;
        moves = poke.battlerMoves;
    }

    public bool hasWorkingAbility(Abilities a)
    {
        return !abilitySuppressed && ability == a;
    }
}
