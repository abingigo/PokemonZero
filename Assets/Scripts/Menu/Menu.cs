using UnityEngine;
using System;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    PlayerMovement pm;
    [SerializeField] GameObject pokemonMenu;
    [SerializeField] GameObject bag;
    public bool inExtension = false, inMenu = false;
    public List<string> options = new List<string>() { "Bag", $"{PlayerProfile.PlayerName}", "Save", "Quit" };
    OptionsBox ob;
    AudioSource audioSource;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        audioSource = pm.gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
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
        if (GameManager.starterSelected)
        {
            options.RemoveAll(r => r == "Pokemon");
            options.Insert(0, "Pokemon");
        }
        if (GameManager.hasPokedex)
            options.Insert(0, "Pokedex");
        ob.addOptions(options.ToArray());
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
            case "Bag":     bag.SetActive(true);
                            bag.GetComponent<BagMenu>().Setup(0);
                            break;
            case "Quit":    Application.Quit();
                            break;
        }
        audioSource.clip = AudioClips.selDecision;
        audioSource.Play();
        inExtension = true;
    }
}
