using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GiveItem : MonoBehaviour, Interactable
{
    [SerializeField] Items item;
    [SerializeField] int amount;

    GameObject player;
    AudioSource audioSource;

    [SerializeField] Dialog dialog;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        audioSource = player.GetComponent<AudioSource>();
    }

    public void interact()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Animator>().SetFloat("speed", 0);
        FindObjectOfType<Menu>().enabled = false;
        StartCoroutine(Interacting());
    }

    IEnumerator Interacting()
    {
        InventoryItem i = new InventoryItem(item);
        i.increaseByAmount(amount - 1);
        PlayerProfile.addItem(i);
        DialogManager dm = DialogManager.Instance;
        audioSource.clip = AudioClips.getItem;
        audioSource.Play();
        dm.ShowDialog(dialog, null, i, audioSource);
        dm.dialogAllowed = false;
        yield return new WaitUntil(() => dm.dialogAllowed);
        player.GetComponent<PlayerMovement>().enabled = true;
        FindObjectOfType<Menu>().enabled = true;
        Destroy(gameObject);
    }
}
