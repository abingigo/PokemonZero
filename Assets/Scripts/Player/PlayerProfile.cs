using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public string PlayerName;
    public int gender;
    public List<InventoryItem> items;
    public FieldPokemon[] party = new FieldPokemon[6];
    public int party_count;
    public int[] badges = new int[8];

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
