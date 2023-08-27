using UnityEngine;

//Contains the progression of the player in the game
//As this is a progression based game, we only require booleans to indicate the progress of the player
//Saving data generally involves serializing the PlayerProfile and the GameManager

public class GameManager : MonoBehaviour
{
    public static bool momEvent = false;
    public static bool oakEvent = false;
    public static bool starterSelected = false;
    public static bool hasPokedex = false;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
