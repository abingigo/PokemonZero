using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSelect : MonoBehaviour, Collidable
{
    [SerializeField] List<Dialog> dialog;
    [SerializeField] GameObject player, NPC, exclamation;
    [SerializeField] Menu menu;

    public void collide()
    {
        if(!GameManager.oakEvent)
        {
            GameManager.oakEvent = true;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Animator>().SetFloat("speed", 0);
            menu.enabled = false;
            StartCoroutine(Event());
        }
        else if(GameManager.oakEvent && !GameManager.starterSelected)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Animator>().SetFloat("speed", 0);
            menu.enabled = false;
            StartCoroutine(Event1());
        }
    }

    IEnumerator Event()
    {
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<Animator>().SetFloat("speed", 1);
        yield return StartCoroutine(player.GetComponent<PlayerMovement>().Move(player.transform.position + new Vector3(0, 3, 0)));
        player.GetComponent<Animator>().SetFloat("speed", 0);
        exclamation.SetActive(true);
        exclamation.transform.position = NPC.transform.position + new Vector3(0, 1);
        exclamation.GetComponent<Animator>().SetBool("Surprise", true);
        player.GetComponent<AudioSource>().clip = AudioClips.exclaim;
        player.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        exclamation.GetComponent<Animator>().SetBool("Surprise", false);
        exclamation.SetActive(false);
        yield return StartCoroutine(Interacting(0));
        player.GetComponent<PlayerMovement>().enabled = true;
        menu.enabled = true;
        player.GetComponent<PlayerMovement>().isColliding = false;
    }

    IEnumerator Event1()
    {
        yield return new WaitForSeconds(0.2f);
        exclamation.SetActive(true);
        exclamation.transform.position = NPC.transform.position + new Vector3(0, 1);
        exclamation.GetComponent<Animator>().SetBool("Surprise", true);
        player.GetComponent<AudioSource>().clip = AudioClips.exclaim;
        player.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        exclamation.GetComponent<Animator>().SetBool("Surprise", false);
        exclamation.SetActive(false);
        yield return StartCoroutine(Interacting(1));
        player.GetComponent<PlayerMovement>().enabled = true;
        menu.enabled = true;
        player.GetComponent<Animator>().SetFloat("speed", 1);
        player.GetComponent<Animator>().SetFloat("Vertical", 1);
        yield return StartCoroutine(player.GetComponent<PlayerMovement>().Move(player.transform.position + new Vector3(0, 1)));
        player.GetComponent<Animator>().SetFloat("speed", 0);
        player.GetComponent<PlayerMovement>().isColliding = false;
    }

    public IEnumerator Interacting(int a)
    {
        DialogManager dm = DialogManager.Instance;
        dm.ShowDialog(dialog[a], null, null);
        dm.dialogAllowed = false;
        yield return new WaitUntil(() => dm.dialogAllowed);
    }
}
