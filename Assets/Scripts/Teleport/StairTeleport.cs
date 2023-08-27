using System.Collections;
using UnityEngine;

//Used when there is no animated entity on teleportation

public class StairTeleport : MonoBehaviour, Collidable
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 pos;
    [SerializeField] AudioClip door_close;
    [SerializeField] Animator blackscreen;
    [SerializeField] Menu menu;
    [SerializeField] Vector2 direction;

    //Referenced in PlayerMovement.cs
    public void collide()
    {
        Animator anim = player.GetComponent<Animator>();
        float h = anim.GetFloat("Horizontal"), v = anim.GetFloat("Vertical");
        if (direction.x != h || direction.y != v)
        {
            player.GetComponent<PlayerMovement>().isColliding = false;
            return;
        }
        player.GetComponent<PlayerMovement>().enabled = false;
        menu.enabled = false;
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        player.GetComponent<AudioSource>().clip = door_close;
        player.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        blackscreen.SetBool("Start", true);
        player.GetComponent<Animator>().SetFloat("speed", 0);
        yield return new WaitForSeconds(2f);
        player.transform.position = pos;
        blackscreen.SetBool("Start", false);
        player.GetComponent<PlayerMovement>().enabled = true;
        menu.enabled = true;
        player.GetComponent<PlayerMovement>().isColliding = false;
    }
}
