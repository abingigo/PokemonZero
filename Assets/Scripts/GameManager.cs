using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool momEvent = false;
    public static bool oakEvent = false;
    public static bool starterSelected = false;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
