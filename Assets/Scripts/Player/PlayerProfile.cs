using System.Collections.Generic;
using UnityEngine;

//Static class used for saving the player data
//Saving data generally involves serializing the PlayerProfile and the GameManager

public static class PlayerProfile
{
    public static string PlayerName;
    public static int gender;
    public static List<InventoryItem> items = new List<InventoryItem>();
    public static FieldPokemon[] party = new FieldPokemon[6];
    public static int party_count;
    public static int[] badges = new int[8];
    public static Vector2 position = Vector2.zero;
    public static float horizontal = 0, vertical = -1;
    public static locations currentLocation;

    public static void addItem(InventoryItem item)
    {
        foreach(InventoryItem i in items)
        {
            if(i.items.Name == item.items.Name)
            {
                i.increaseByAmount(item.count);
                return;
            }
        }
        items.Add(item);
    }

    public static void addPokemon(FieldPokemon fp)
    {
        if(party_count < 6)
            party[party_count++] = fp;
    }
}