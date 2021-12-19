using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExitTeleport : MonoBehaviour, Collidable
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 pos;
    [SerializeField] Animator blackscreen, door;
    [SerializeField] Menu menu;
    [SerializeField] AudioClip newBGM;
    [SerializeField] GameObject mainCamera;

    private void Start()
    {
        mainCamera = FindObjectOfType<StartGame>().gameObject;
    }

    public void collide()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        menu.enabled = false;
        door.SetBool("Open", true);
        StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        player.GetComponent<AudioSource>().clip = AudioClips.doorExit;
        player.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        blackscreen.SetBool("Start", true);
        player.GetComponent<Animator>().SetFloat("speed", 0);
        yield return new WaitForSeconds(2f);
        player.transform.position = pos;
        blackscreen.SetBool("Start", false);
        mainCamera.GetComponentInChildren<AudioSource>().clip = newBGM;
        mainCamera.GetComponentInChildren<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        if(door != null)
        {
            door.SetBool("Close", true);
            yield return new WaitForSeconds(0.5f);
            door.SetBool("Open", false);
            door.SetBool("Close", false);
        }
        player.GetComponent<PlayerMovement>().enabled = true;
        menu.enabled = true;
        player.GetComponent<PlayerMovement>().isColliding = false;
    }
}
