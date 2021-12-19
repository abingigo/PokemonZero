using System.Collections;
using UnityEngine;

public class MomEvent : MonoBehaviour, Collidable
{
    [SerializeField] GameObject player, NPC;
    [SerializeField] Menu menu;
    [SerializeField] GameObject exclamation;

    public void collide()
    {
        if(!GameManager.momEvent)
        {
            GameManager.momEvent = true;
            player.GetComponent<PlayerMovement>().enabled = false;
            menu.enabled = false;
            StartCoroutine(Event());
        }
    }

    IEnumerator Event()
    {
        yield return new WaitForSeconds(0.5f);
        exclamation.SetActive(true);
        exclamation.transform.position = NPC.transform.position + new Vector3(0, 1);
        exclamation.GetComponent<Animator>().SetBool("Surprise", true);
        player.GetComponent<AudioSource>().clip = AudioClips.exclaim;
        player.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        exclamation.GetComponent<Animator>().SetBool("Surprise", false);
        exclamation.SetActive(false);
        yield return StartCoroutine(NPC.GetComponent<NPCController>().MoveToPlayer());
        yield return StartCoroutine(NPC.GetComponent<NPCController>().Interacting(0));
        player.GetComponent<PlayerMovement>().enabled = true;
        menu.enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<PlayerMovement>().isColliding = false;
    }
}
