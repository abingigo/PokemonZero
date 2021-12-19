using UnityEngine;

public class Layers : MonoBehaviour
{
    public static LayerMask obstacles;
    public static LayerMask interactable;
    public static LayerMask collidable;
    public static LayerMask encounters;

    private void Awake()
    {
        obstacles = LayerMask.GetMask("Obstacles");
        interactable = LayerMask.GetMask("Interactable");
        collidable = LayerMask.GetMask("Collidable");
        encounters = LayerMask.GetMask("Encounter");
    }
}
