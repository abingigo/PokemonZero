using UnityEngine;

public enum positions {bottom, bottomleft, bottomright, topright, abovebottomright};

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public PlayerMovement pm;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject pokemonMenu;
    int i = 0;
    bool inMenu = false;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !inMenu)
        {
            menu.SetActive(true);
            AudioSource audioSource = pm.gameObject.GetComponent<AudioSource>();
            pm.enabled = false;
            inMenu = true;
            audioSource.clip = FindObjectOfType<AudioClips>().menuOpen;
            audioSource.Play();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && inMenu)
        {
            menu.SetActive(false);
            pm.enabled = true;
            inMenu = false;
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, 83, arrow.transform.localPosition.z);
            i = 0;
            pm.gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().menuClose;
            pm.gameObject.GetComponent<AudioSource>().Play();
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && i > 0 && inMenu)
        {
            i--;
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y + 45, arrow.transform.localPosition.z);
            pm.gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCursor;
            pm.gameObject.GetComponent<AudioSource>().Play();
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && i < 5 && inMenu)
        {
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y - 45, arrow.transform.localPosition.z);
            i++;
            pm.gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selCursor;
            pm.gameObject.GetComponent<AudioSource>().Play();
        }

        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && inMenu)
        {
            if (i == 6)
                Application.Quit();
            if(i == 1)
            {
                pokemonMenu.SetActive(true);
                pokemonMenu.GetComponent<PokemonMenu>().ShowMenu();
            }
            inMenu = false;
            menu.SetActive(false);
            i = 0;
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, 83, arrow.transform.localPosition.z);
            pm.gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<AudioClips>().selDecision;
            pm.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
