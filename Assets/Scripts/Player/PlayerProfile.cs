using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static string PlayerName;
    public static int gender;
    public static List<InventoryItem> items;
    public static FieldPokemon[] party;
    public static int party_count;
    public static int[] badges = new int[8];
    public static Vector2 position = Vector2.zero;
    public static float horizontal = 0, vertical = -1;

    private void Start()
    {
        party = new FieldPokemon[6];
        DontDestroyOnLoad(gameObject);
    }
}