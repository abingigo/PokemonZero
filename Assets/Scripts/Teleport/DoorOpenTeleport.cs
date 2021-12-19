using System.Collections;
using UnityEngine;

public class DoorOpenTeleport : MonoBehaviour, Collidable
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 pos;
    [SerializeField] Animator blackscreen;
    [SerializeField] Menu menu;
    [SerializeField] AudioClip newBGM;
    GameObject mainCamera;

    private void Start()
    {
        mainCamera = FindObjectOfType<StartGame>().gameObject;
    }

    public void collide()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        menu.enabled = false;
        GetComponent<Animator>().SetBool("Open", true);
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        player.GetComponent<AudioSource>().clip = AudioClips.doorEnter;
        player.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        blackscreen.SetBool("Start", true);
        player.GetComponent<Animator>().SetFloat("speed", 0);
        yield return new WaitForSeconds(2f);
        player.transform.position = pos;
        blackscreen.SetBool("Start", false);
        player.GetComponent<PlayerMovement>().enabled = true;
        menu.enabled = true;
        mainCamera.GetComponentInChildren<AudioSource>().clip = newBGM;
        mainCamera.GetComponentInChildren<AudioSource>().Play();
        player.GetComponent<PlayerMovement>().isColliding = false;
    }
}
