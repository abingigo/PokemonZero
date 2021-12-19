using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public GameObject background, oak, marill, boy, girl, Base;
    public AudioClip ac;
    public GameObject mainCamera;

    [TextArea(3, 10)]
    public string[] dialog;
    public Queue<string> sentences;

    public Text textField;
    public GameObject arrow;
    public GameObject speechBox;
    public GameObject genderSelect;
    public GameObject arrow1;
    int i = 0;
    public GameObject inputField;

    private void Start()
    {
        sentences = new Queue<string>();
        foreach(string s in dialog)
            sentences.Enqueue(s);
        background.GetComponent<Animator>().SetBool("BGActive", true);
        StartCoroutine(Wait());
    }

    public void Update()
    {
        if(!inputField.activeInHierarchy)
        {
            if (!genderSelect.activeInHierarchy)
            {
                if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && arrow.activeInHierarchy)
                {
                    mainCamera.GetComponent<AudioSource>().clip = ac;
                    mainCamera.GetComponent<AudioSource>().Play();
                    ShowNextSentence();
                    arrow.SetActive(false);
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && i == 1)
                {
                    i--;
                    arrow1.transform.localPosition = new Vector3(arrow1.transform.localPosition.x, arrow1.transform.localPosition.y + 40, arrow1.transform.localPosition.z);
                }
                else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && i == 0)
                {
                    i++;
                    arrow1.transform.localPosition = new Vector3(arrow1.transform.localPosition.x, arrow1.transform.localPosition.y - 40, arrow1.transform.localPosition.z);
                }
                else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
                {
                    PlayerProfile.gender = i;
                    genderSelect.SetActive(false);
                    if(i == 0)
                        boy.GetComponent<Animator>().SetBool("BoyActive", true);
                    else
                        girl.GetComponent<Animator>().SetBool("GirlActive", true);
                    ShowNextSentence();
                }
            }
        }
    }

    public void ShowNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        if (sentence == "MarillIn")
        {
            speechBox.SetActive(false);
            oak.GetComponent<Animator>().SetBool("OakActive", false);
            marill.GetComponent<Animator>().SetBool("MarillActive", true);
            StartCoroutine(Wait1());
        }
        else if(sentence == "OakIn")
        {
            speechBox.SetActive(false);
            marill.GetComponent<Animator>().SetBool("MarillActive", false);
            oak.GetComponent<Animator>().SetBool("OakActive", true);
            StartCoroutine(Wait1());
        }
        else if (sentence == "BoyGirl")
        {
            speechBox.SetActive(false);
            oak.GetComponent<Animator>().SetBool("OakActive", false);
            StartCoroutine(Wait1());
        }
        else if (sentence == "Ask")
        {
            genderSelect.SetActive(true);
        }
        else if (sentence == "GetName")
        {
            inputField.SetActive(true);
        }
        else if (sentence == "PlayerName")
        {
            if (speechBox.activeInHierarchy == false)
                speechBox.SetActive(true);
            StartCoroutine(TypeSentence($"So you are {PlayerProfile.PlayerName}?"));
        }
        else if (sentence == "RDY")
        {
            if (speechBox.activeInHierarchy == false)
                speechBox.SetActive(true);
            StartCoroutine(TypeSentence($"{PlayerProfile.PlayerName}, are you ready?"));
        }
        else
        {
            if (speechBox.activeInHierarchy == false)
                speechBox.SetActive(true);
            StartCoroutine(TypeSentence(sentence));
        }
    }

    public void EndEdit()
    {
        PlayerProfile.PlayerName = inputField.GetComponent<InputField>().text;
        inputField.SetActive(false);
        ShowNextSentence();
    }

    public void EndDialog()
    {
        speechBox.SetActive(false);
        boy.GetComponent<Animator>().SetBool("BoyActive", false);
        girl.GetComponent<Animator>().SetBool("GirlActive", false);
        background.GetComponent<Animator>().SetBool("BGActive", false);
        Base.GetComponent<Animator>().SetBool("BaseActive", false);
        StartCoroutine(Wait2());
    }

    IEnumerator TypeSentence(string sentence)
    {
        textField.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            textField.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        arrow.SetActive(true);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        Base.GetComponent<Animator>().SetBool("BaseActive", true);
        oak.GetComponent<Animator>().SetBool("OakActive", true);
        yield return new WaitForSeconds(1f);
        speechBox.SetActive(true);
        ShowNextSentence();
    }

    IEnumerator Wait1()
    {
        yield return new WaitForSeconds(1);
        ShowNextSentence();
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
