using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    float speed = 2f;
    Vector2 movement;
    bool horizontal = false, vertical = false, isMoving = false;
    public bool isColliding = false, isInteracting = false;
    AudioSource source;
    Animator animator;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 targetpos = Vector3.zero;
        if (!isMoving)
        {
            if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                if (horizontal)
                {
                    movement.y = Input.GetAxisRaw("Vertical");
                    movement.x = 0;
                }
                else if (vertical)
                {
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = 0;
                }
            }
            else
            {
                horizontal = Input.GetAxisRaw("Horizontal") != 0;
                movement.x = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical") != 0;
                movement.y = Input.GetAxisRaw("Vertical");
            }
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
            }

            if (movement != Vector2.zero)
            {
                targetpos = transform.position + new Vector3(movement.x, movement.y, 0);
                if (isWalkable(targetpos - new Vector3(0,0.5f,0)))
                    StartCoroutine(Move(targetpos));
            }
            animator.SetFloat("speed", movement.sqrMagnitude);
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) && !isInteracting)
            Interact();
    }

    public IEnumerator Move(Vector3 targetpos)
    {
        isMoving = true;
        while ((targetpos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetpos, speed * Time.fixedDeltaTime);
            yield return new WaitForEndOfFrame();
        }
        transform.position = targetpos;
        isMoving = false;
        Encounter();
        if(!isColliding)
            Collide();
    }

    private bool isWalkable(Vector3 targetpos)
    {
        if (Physics2D.OverlapBox(targetpos, new Vector2(1, 0.2f), 45, Layers.obstacles | Layers.interactable) != null)
        {
            if(!source.isPlaying)
            {
                source.clip = AudioClips.bump;
                source.Play();
            }
            return false;
        }
        return true;
    }

    void Interact()
    {
        var faceDir = new Vector3(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
        var interactpos = transform.position - new Vector3(0, 0.5f, 0) + faceDir;

        var collider = Physics2D.OverlapBox(interactpos, new Vector2(1, 0.1f), 90, Layers.interactable);
        if (collider != null)
        {
            isInteracting = true;
            collider.GetComponent<Interactable>()?.interact();
        }
    }

    void Collide()
    {
        var faceDir = new Vector3(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
        var interactpos = transform.position - new Vector3(0, 0.5f, 0) + faceDir;

        var collider = Physics2D.OverlapBox(interactpos, new Vector2(1, 0.1f), 90, Layers.collidable);
        if (collider != null)
        {
            Debug.Log("Bonk");
            isColliding = true;
            collider.GetComponent<Collidable>()?.collide();
        }
    }

    void Encounter()
    {
        if(PlayerProfile.party_count == 0)
            return;
        var collider = Physics2D.OverlapBox(transform.position - new Vector3(0, 0.5f, 0), new Vector2(1, 0.1f), 90, Layers.encounters);
        if(collider != null)
            collider.GetComponent<Encounter>()?.encounter();
    }
}
