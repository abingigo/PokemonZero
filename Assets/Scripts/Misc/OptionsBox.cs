using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum positions {menu, choice};

public class OptionsBox : MonoBehaviour
{
    public List<string> Options;
    public positions p;

    [SerializeField] GameObject box;
    [SerializeField] GameObject option;
    [SerializeField] GameObject arrow;

    public static OptionsBox Instance { get; private set; }
    GameObject optionsBox, opt, arr;
    bool choosing;
    Action<string> recieve;
    int i;

    void Awake()
    {
        Options = new List<string>();
        Instance = this;
        choosing = false;
    }

    private void Update()
    {
        if(choosing)
        {
            if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && i > 0)
            {
                i--;
                arr.transform.localPosition = new Vector3(arr.transform.localPosition.x, arr.transform.localPosition.y + 40, arr.transform.localPosition.z);
            }
            if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && i < Options.Count - 1)
            {
                i++;
                arr.transform.localPosition = new Vector3(arr.transform.localPosition.x, arr.transform.localPosition.y - 40, arr.transform.localPosition.z);
            }
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
                Invoke("sendChoice", 0.1f);
        }
    }

    public void addOptions(string[] s)
    {
        for(int i = 0; i < s.Length; i++)
            Options.Add(s[i]);
    }

    public void ShowOptions(Transform parent, Action<string> rec)
    {
        if(p == positions.menu)
        {
            optionsBox = Instantiate(box, Vector3.zero, Quaternion.identity);
            optionsBox.transform.SetParent(parent);
            RectTransform rt = optionsBox.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(225, 40 * (Options.Count + 1) + 10);
            rt.anchoredPosition = new Vector2(-125, - 30 - Options.Count * 25);
            rt.anchorMin = new Vector2(1, 1);
            rt.anchorMax = new Vector2(1, 1);
            rt.localScale = new Vector3(1,1,1);
        }
        else if(p == positions.choice)
        {
            optionsBox = Instantiate(box, Vector3.zero, Quaternion.identity);
            optionsBox.transform.SetParent(parent);
            RectTransform rt = optionsBox.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(200, 40 * (Options.Count + 1) + 10);
            rt.anchoredPosition = new Vector2(295, - 100 - (Options.Count - 1) * 20);
            rt.localScale = new Vector3(1,1,1);
        }
        
        for(int i = 0; i < Options.Count; i++)
        {
            opt = Instantiate(option, Vector3.zero, Quaternion.identity);
            opt.transform.SetParent(optionsBox.transform);
            RectTransform rt1 = opt.GetComponent<RectTransform>();
            rt1.sizeDelta = new Vector2(130, 40);
            rt1.anchoredPosition = new Vector2(20, 0.5f * (Options.Count - 1) * 40 - 40 * i);
            rt1.localScale = new Vector3(1,1,1);
            opt.GetComponent<TextMeshProUGUI>().text = Options[i];
        }
        arr = Instantiate(arrow, Vector3.zero, Quaternion.identity);
        arr.transform.SetParent(optionsBox.transform);
        RectTransform rt2 = arr.GetComponent<RectTransform>();
        rt2.sizeDelta = new Vector2(15.2361f, 28);
        rt2.anchoredPosition = new Vector2(-72, 0.5f * (Options.Count - 1) * 40);
        rt2.localScale = new Vector3(1.5f,1.5f,1);
        choosing = true;
        i = 0;
        recieve = rec;
    }

    public void destroy()
    {
        Options = new List<string>();
        choosing = false;
        Destroy(optionsBox);
    }

    void sendChoice()
    {
        choosing = false;
        recieve(Options[i]);
        destroy();
    }
}
