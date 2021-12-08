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
    [SerializeField] GameObject yesNo;

    public void interact()
    {
        if(!GameObject.FindObjectOfType<GameManager>().starterSelected)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Animator>().SetFloat("speed", 0);
            menu.enabled = false;
            StartCoroutine(Event());
        }
        else
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Animator>().SetFloat("speed", 0);
            menu.enabled = false;
            StartCoroutine(Interacting(1));
        }
    }

    IEnumerator Event()
    {
        yield return StartCoroutine(Interacting(2));
        yesNo.SetActive(true);
        yesNo.GetComponent<YesNo>().answered = false;
        yield return new WaitUntil(() => yesNo.GetComponent<YesNo>().answered);
        if (yesNo.GetComponent<YesNo>().i == 1)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            menu.enabled = true;
            player.GetComponent<PlayerMovement>().isInteracting = false;
            yesNo.SetActive(false);
            yield break;
        }
        yesNo.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        FindObjectOfType<GameManager>().starterSelected = true;
        FindObjectOfType<PlayerProfile>().party[0] = new FieldPokemon(pokemon, "", -1, -1, -1, -1, 5, false);
        FindObjectOfType<PlayerProfile>().party_count = 1;
        player.GetComponent<AudioSource>().clip = pokemonGet;
        player.GetComponent<AudioSource>().Play();
        yield return StartCoroutine(Interacting(0));
    }

    public IEnumerator Interacting(int a)
    {
        DialogManager dm = DialogManager.Instance;
        dm.ShowDialog(dialog[a], null, null);
        dm.dialogAllowed = false;
        yield return new WaitUntil(() => dm.dialogAllowed);
        if(a != 2)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            menu.enabled = true;
            player.GetComponent<PlayerMovement>().isInteracting = false;
        }
    }
}