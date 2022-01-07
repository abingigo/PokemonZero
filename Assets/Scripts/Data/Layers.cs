using UnityEngine;

public static class Layers
{
    public static LayerMask obstacles;
    public static LayerMask interactable;
    public static LayerMask collidable;
    public static LayerMask encounters;

    static Layers()
    {
        obstacles = LayerMask.GetMask("Obstacles");
        interactable = LayerMask.GetMask("Interactable");
        collidable = LayerMask.GetMask("Collidable");
        encounters = LayerMask.GetMask("Encounter");
    }
}
