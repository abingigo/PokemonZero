using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePokemon : MonoBehaviour, Interactable
{
    [SerializeField] Pokemon pokemon;
    [SerializeField] GameObject player;
    [SerializeField] Menu menu;
    [SerializeField] AudioClip pokemonGet;
    [SerializeField] List<Dialog> dialog;
    OptionsBox ob;
    bool answered = false;
    int i = -1;

    PlayerMovement pm;
    AudioSource audioSource;
    Animator anim;

    private void Awake()
    {
        pm = player.GetComponent<PlayerMovement>();
        audioSource = player.GetComponent<AudioSource>();
        anim = player.GetComponent<Animator>();
    }

    public void interact()
    {
        if(!GameManager.starterSelected)
        {
            pm.enabled = false;
            anim.SetFloat("speed", 0);
            menu.enabled = false;
            StartCoroutine(Event());
        }
        else
        {
            pm.enabled = false;
            anim.SetFloat("speed", 0);
            menu.enabled = false;
            StartCoroutine(Interacting(1));
        }
    }

    IEnumerator Event()
    {
        yield return StartCoroutine(Interacting(2));

        yield return new WaitUntil(() => answered);

        if(i == 1)
        {
            pm.enabled = true;
            menu.enabled = true;
            pm.isInteracting = false;
            answered = false;
            i = -1;
            yield break;
        }

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GameManager.starterSelected = true;
        PlayerProfile.addPokemon(new FieldPokemon(pokemon, 5));
        yield return StartCoroutine(gotPokemon());
    }

    public IEnumerator Interacting(int a)
    {
        DialogManager dm = DialogManager.Instance;
        if(a == 2)
            dm.AskQuestion(dialog[a], new string[]{"Yes", "No"}, recieveOption);
        else
            dm.ShowDialog(dialog[a], null, null, audioSource);
        dm.dialogAllowed = false;
        yield return new WaitUntil(() => dm.dialogAllowed);

        if(a == 1)
        {
            pm.enabled = true;
            menu.enabled = true;
            pm.isInteracting = false;
        }
    }

    IEnumerator gotPokemon()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.clip = pokemonGet;
        audioSource.Play();
        StartCoroutine(Interacting(0));
        yield return new WaitWhile (()=> audioSource.isPlaying);
        pm.enabled = true;
        menu.enabled = true;
        pm.isInteracting = false;
    }

    void recieveOption(string s)
    {
        switch(s)
        {
            case "Yes": i = 0;
                        break;
            case "No": i = 1;
                       break;
        }
        answered = true;
    }
}
