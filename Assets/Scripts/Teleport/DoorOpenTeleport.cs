using System.Collections;
using UnityEngine;

public class DoorOpenTeleport : MonoBehaviour, Collidable
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 pos;
    [SerializeField] AudioClip door_open;
    [SerializeField] Animator blackscreen;
    [SerializeField] Menu menu;
    [SerializeField] AudioClip newBGM;

    public void collide()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        menu.enabled = false;
        GetComponent<Animator>().SetBool("Open", true);
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        player.GetComponent<AudioSource>().clip = door_open;
        player.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        blackscreen.SetBool("Start", true);
        player.GetComponent<Animator>().SetFloat("speed", 0);
        yield return new WaitForSeconds(2f);
        player.transform.position = pos;
        blackscreen.SetBool("Start", false);
        player.GetComponent<PlayerMovement>().enabled = true;
        menu.enabled = true;
        player.GetComponentInChildren<AudioSource>().clip = newBGM;
        player.GetComponentInChildren<AudioSource>().Play();
        player.GetComponent<PlayerMovement>().isColliding = false;
    }
}
