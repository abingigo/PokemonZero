using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool momEvent = false;
    public bool oakEvent = false;
    public bool starterSelected = false;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
