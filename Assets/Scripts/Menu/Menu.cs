using UnityEngine;
using System;

public class Menu : MonoBehaviour
{
    PlayerMovement pm;
    [SerializeField] GameObject pokemonMenu;
    public bool inExtension = false, inMenu = false;
    OptionsBox ob;
    AudioSource audioSource;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        audioSource = pm.gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if(!inExtension)
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !inMenu)
            {
                ShowMenu();
                audioSource.clip = AudioClips.menuOpen;
                audioSource.Play();
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && inMenu)
            {
                ob.destroy();
                pm.enabled = true;
                inMenu = false;
                audioSource.clip = AudioClips.menuClose;
                audioSource.Play();
            }
        }
    }

    public void ShowMenu()
    {
        ob = OptionsBox.Instance;
        if(GameManager.starterSelected)
            ob.addOptions(new string[] {"Pokemon", "Bag", $"{PlayerProfile.PlayerName}", "Save", "Quit"});
        else
            ob.addOptions(new string[] {"Bag", $"{PlayerProfile.PlayerName}", "Save", "Quit"});
        ob.p = positions.menu;
        ob.ShowOptions(transform, recieveChoice);
        pm.enabled = false;
        inMenu = true;
        inExtension = false;
    }

    public void recieveChoice(string s)
    {
        switch(s)
        {
            case "Pokemon": if(PlayerProfile.party[0].pokemon != null)
                            {
                                pokemonMenu.SetActive(true);
                                pokemonMenu.GetComponent<PokemonMenu>().ShowMenu();
                            }
                            break;
            case "Bag":     Debug.Log("djhfjd");
                            break;
            case "Quit":    Application.Quit();
                            break;
        }
        audioSource.clip = AudioClips.selDecision;
        audioSource.Play();
        inExtension = true;
    }
}
