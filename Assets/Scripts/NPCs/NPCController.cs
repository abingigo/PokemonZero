using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> dialogs;
    [SerializeField] Pokemon pokemon;
    [SerializeField] InventoryItem inventoryItem;
    bool isInteracting = false;
    [SerializeField] GameObject player;
    [SerializeField] List<Vector3> positions;
    Animator anim;
    bool isMoving;
    AudioSource audioSource;

    public void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = player.GetComponent<AudioSource>();
    }

    public void interact()
    {
        if(!isInteracting)
        {
            StopAllCoroutines();
            StartCoroutine(Interacting(1));
            FacePlayer();
        }
    }

    public IEnumerator Interacting(int a)
    {
        isInteracting = true;
        DialogManager dm = DialogManager.Instance;
        dm.ShowDialog(dialogs[a], pokemon, inventoryItem, audioSource);
        dm.dialogAllowed = false;
        yield return new WaitUntil(() => dm.dialogAllowed);
        isInteracting = false;
        player.GetComponent<PlayerMovement>().isInteracting = false;
    }

    public void FacePlayer()
    {
        Animator anim1 = player.GetComponent<Animator>();
        anim.SetFloat("Horizontal", anim1.GetFloat("Horizontal") * -1);
        anim.SetFloat("Vertical", anim1.GetFloat("Vertical") * -1);
        anim.SetFloat("speed", 0);
    }

    public IEnumerator MoveToPlayer()
    {
        foreach (Vector3 position in positions)
        {
            Vector3 x = transform.position - position;
            if(Mathf.Abs(x.x) < Mathf.Epsilon)
            {
                GetComponent<Animator>().SetFloat("Horizontal", 0);
                GetComponent<Animator>().SetFloat("Vertical", -1 * x.y / Mathf.Abs(x.y));
            }
            else
            {
                GetComponent<Animator>().SetFloat("Horizontal", 1 * x.x / Mathf.Abs(x.x));
                GetComponent<Animator>().SetFloat("Vertical", 0);
            }
            isMoving = true;
            GetComponent<Animator>().SetFloat("speed", 1);
            StartCoroutine(Move(position));
            yield return new WaitUntil(() => !isMoving);
        }
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetFloat("speed", 0);
        FacePlayer();
    }

    IEnumerator Move(Vector3 targetpos)
    {
        isMoving = true;
        while ((targetpos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetpos, Time.fixedDeltaTime);
            yield return null;
        }
        transform.position = targetpos;
        isMoving = false;
    }

    private bool isWalkable(Vector3 targetpos)
    {
        if (Physics2D.OverlapCircle(targetpos, 0.3f, Layers.obstacles | Layers.interactable) != null)
            return false;
        return true;
    }
}
