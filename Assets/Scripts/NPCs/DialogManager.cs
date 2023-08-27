using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogText;
    Queue<string> sentences;
    [SerializeField] GameObject arrow;

    InventoryItem item;
    Pokemon pokemon;

    [SerializeField] GameObject player;

    bool inDialog = false;
    bool asking = false, asked = false;
    public bool dialogAllowed = true;

    public static DialogManager Instance { get; private set; }
    
    OptionsBox ob;
    string[] s;
    Action<string> reci;

    private void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
    }

    public void Update()
    {
        if (inDialog)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && (arrow.activeInHierarchy || asked))
            {
                if(!player.GetComponent<AudioSource>().isPlaying)
                {
                    player.GetComponent<AudioSource>().clip = AudioClips.selCursor;
                    player.GetComponent<AudioSource>().Play();
                }
                ShowNextSentence(null);
                arrow.SetActive(false);
                asking = asked = false;
            }
        }
    }

    public void ShowDialog(Dialog dialog, Pokemon p, InventoryItem inventoryItem, AudioSource audioSource = null)
    {
        pokemon = p;
        item = inventoryItem;
        dialogBox.SetActive(true);
        sentences.Clear();
        foreach (string d in dialog.Lines)
            sentences.Enqueue(d);
        inDialog = true;
        ShowNextSentence(audioSource);
    }

    public void AskQuestion(Dialog dialog, string[] options, Action<string> rec, Pokemon p = null, InventoryItem inventoryItem = null, AudioSource audioSource = null)
    {
        pokemon = p;
        item = inventoryItem;
        dialogBox.SetActive(true);
        sentences.Clear();
        foreach (string d in dialog.Lines)
            sentences.Enqueue(d);
        inDialog = true;
        ShowNextSentence(audioSource);
        s = options;
        reci = rec;
        asking = true;
    }

    public void ShowNextSentence(AudioSource audioSource)
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        sentence = sentence.Replace("$pname", PlayerProfile.PlayerName);
        if (sentence != "GiveItem" && sentence != "GivePokemon")
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence, audioSource));
        }
        else if (sentence == "GiveItem")
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence($"Obtained a {item.items.Name}", audioSource));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence($"Obtained a {pokemon.Name}", audioSource));
        }
    }

    IEnumerator TypeSentence(string sentence, AudioSource audioSource)
    {
        dialogText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        if(audioSource != null)
            yield return new WaitWhile (()=> audioSource.isPlaying);
        if(!asking)
            arrow.SetActive(true);
        else
            Ask();
    }

    void Ask()
    {
        ob = OptionsBox.Instance;
        ob.addOptions(s);
        ob.p = positions.choice;
        ob.ShowOptions(FindObjectOfType<Menu>().transform, reci);
        asked = true;
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
        inDialog = false;
        dialogAllowed = true;
    }
}
