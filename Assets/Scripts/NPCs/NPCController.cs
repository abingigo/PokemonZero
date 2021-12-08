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
    Animator anim;
    Vector2 movement;
    Vector3 targetpos;
    bool isMoving;

    public void Start()
    {
        anim = GetComponent<Animator>();
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
        dm.ShowDialog(dialogs[a], pokemon, inventoryItem);
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
        movement = transform.position - player.transform.position - new Vector3(player.GetComponent<Animator>().GetFloat("Horizontal"), player.GetComponent<Animator>().GetFloat("Vertical"));
        
        while(movement != Vector2.zero)
        {
            isMoving = true;
            GetComponent<Animator>().SetFloat("speed", 1);
            if(player.GetComponent<Animator>().GetFloat("Horizontal") == 0)
            {
                while (movement.x != 0)
                {
                    targetpos = transform.position + new Vector3(1, 0, 0);
                    GetComponent<Animator>().SetFloat("Horizontal", 1);
                    GetComponent<Animator>().SetFloat("Vertical", 0);
                    if (isWalkable(targetpos - new Vector3(0, 0.3f, 0)))
                        StartCoroutine(Move(targetpos));
                    else
                        break;
                    yield return new WaitUntil(() => !isMoving);
                    if (movement.x < 0)
                        movement += new Vector2(1, 0);
                    else
                        movement += new Vector2(-1, 0);
                }

                while (movement.y != 0)
                {
                    targetpos = transform.position + new Vector3(0, 1, 0);
                    GetComponent<Animator>().SetFloat("Horizontal", 0);
                    GetComponent<Animator>().SetFloat("Vertical", 1);
                    if (isWalkable(targetpos - new Vector3(0, 0.3f, 0)))
                        StartCoroutine(Move(targetpos));
                    else
                        break;
                    yield return new WaitUntil(() => !isMoving);
                    if (movement.y < 0)
                        movement += new Vector2(0, 1);
                    else
                        movement += new Vector2(0, -1);
                }
            }
            else
            {
                while (movement.y != 0)
                {
                    targetpos = transform.position + new Vector3(0, 1, 0);
                    GetComponent<Animator>().SetFloat("Horizontal", 0);
                    GetComponent<Animator>().SetFloat("Vertical", 1);
                    if (isWalkable(targetpos - new Vector3(0, 0.3f, 0)))
                        StartCoroutine(Move(targetpos));
                    else
                        break;
                    yield return new WaitUntil(() => !isMoving);
                    if (movement.y < 0)
                        movement += new Vector2(0, 1);
                    else
                        movement += new Vector2(0, -1);
                }

                while (movement.x != 0)
                {
                    targetpos = transform.position + new Vector3(1, 0, 0);
                    GetComponent<Animator>().SetFloat("Horizontal", 1);
                    GetComponent<Animator>().SetFloat("Vertical", 0);
                    if (isWalkable(targetpos - new Vector3(0, 0.3f, 0)))
                        StartCoroutine(Move(targetpos));
                    else
                        break;
                    yield return new WaitUntil(() => !isMoving);
                    if (movement.x < 0)
                        movement += new Vector2(1, 0);
                    else
                        movement += new Vector2(-1, 0);
                }
            }
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
            transform.position = Vector3.MoveTowards(transform.position, targetpos, 2 * Time.fixedDeltaTime);
            yield return null;
        }
        transform.position = targetpos;
        isMoving = false;
    }

    private bool isWalkable(Vector3 targetpos)
    {
        if (Physics2D.OverlapCircle(targetpos, 0.3f, GameObject.FindObjectOfType<Layers>().obstacles | GameObject.FindObjectOfType<Layers>().interactable) != null)
            return false;
        return true;
    }
}
