using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    Queue<string> sentences;
    [SerializeField] GameObject arrow;

    InventoryItem item;
    Pokemon pokemon;

    [SerializeField] GameObject player;
    PlayerProfile pp;
    [SerializeField] Menu menu;

    bool inDialog = false;
    public bool dialogAllowed = true;

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        pp = GameObject.FindObjectOfType<PlayerProfile>();
        Instance = this;
        sentences = new Queue<string>();
    }

    public void Update()
    {
        if (inDialog)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && arrow.activeInHierarchy)
            {
                if(!player.GetComponent<AudioSource>().isPlaying)
                {
                    player.GetComponent<AudioSource>().clip = FindObjectOfType<AudioClips>().selCursor;
                    player.GetComponent<AudioSource>().Play();
                }
                ShowNextSentence();
                arrow.SetActive(false);
            }
        }
    }

    public void ShowDialog(Dialog dialog, Pokemon p, InventoryItem inventoryItem)
    {
        pokemon = p;
        item = inventoryItem;
        dialogBox.SetActive(true);
        sentences.Clear();
        foreach (string d in dialog.Lines)
            sentences.Enqueue(d);
        inDialog = true;
        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        sentence = sentence.Replace("$pname", pp.PlayerName);
        if (sentence != "GiveItem" && sentence != "GivePokemon")
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else if (sentence == "GiveItem")
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence($"Obtained a {item.items.Name}"));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence($"Obtained a {pokemon.Name}"));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        arrow.SetActive(true);
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
        inDialog = false;
        dialogAllowed = true;
    }
}
