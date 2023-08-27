using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public enum positions {menu, choice, pokemonchoice};

public class OptionsBox : MonoBehaviour
{
    public List<string> Options;
    public positions p;

    [SerializeField] GameObject box;
    [SerializeField] GameObject option;
    [SerializeField] GameObject arrow;

    public static OptionsBox Instance { get; private set; }
    GameObject optionsBox, opt, arr;
    public bool choosing;
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
                arr.transform.localPosition = new Vector3(arr.transform.localPosition.x, arr.transform.localPosition.y + 30, arr.transform.localPosition.z);
            }
            if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && i < Options.Count - 1)
            {
                i++;
                arr.transform.localPosition = new Vector3(arr.transform.localPosition.x, arr.transform.localPosition.y - 30, arr.transform.localPosition.z);
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
        RectTransform rt;
        if(p == positions.menu)
        {
            optionsBox = Instantiate(box, Vector3.zero, Quaternion.identity);
            optionsBox.transform.SetParent(parent);
            rt = optionsBox.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(1, 30 * (Options.Count + 1) + 15);
            rt.anchoredPosition = new Vector2(256, 140 - (Options.Count - 1) * 15);
            rt.localScale = new Vector3(1,1,1);
        }
        else if(p == positions.choice)
        {
            optionsBox = Instantiate(box, Vector3.zero, Quaternion.identity);
            optionsBox.transform.SetParent(parent);
            rt = optionsBox.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(1, 30 * (Options.Count + 1) + 15);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = new Vector2(256, - 75 + (Options.Count - 1) * 20);
            rt.localScale = new Vector3(1,1,1);
        }
        else if(p == positions.pokemonchoice)
        {
            optionsBox = Instantiate(box, Vector3.zero, Quaternion.identity);
            optionsBox.transform.SetParent(parent);
            rt = optionsBox.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(1, 30 * (Options.Count + 1) + 15);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = new Vector2(256, - 100 + (Options.Count - 1) * 20);
            rt.localScale = new Vector3(1,1,1);
        }

        string s = Options.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
        opt = Instantiate(option, Vector3.zero, Quaternion.identity);
        opt.transform.SetParent(optionsBox.transform);
        opt.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 20);
        opt.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        opt.GetComponent<TextMeshProUGUI>().text = s;
        opt.GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
        float length = opt.GetComponent<TextMeshProUGUI>().textBounds.size.x;
        Destroy(opt);

        rt = optionsBox.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(length + 64, rt.sizeDelta.y);
        rt.anchoredPosition = new Vector2(256 - rt.sizeDelta.x / 2 - 20, rt.anchoredPosition.y);

        for(int i = 0; i < Options.Count; i++)
        {
            opt = Instantiate(option, Vector3.zero, Quaternion.identity);
            opt.transform.SetParent(optionsBox.transform);
            
            RectTransform rt1 = opt.GetComponent<RectTransform>();
            TextMeshProUGUI tmp = opt.GetComponent<TextMeshProUGUI>();

            rt1.sizeDelta = new Vector2(130, 20);
            tmp.ForceMeshUpdate();
            rt1.localScale = new Vector3(1,1,1);
            tmp.text = Options[i];
            rt1.anchoredPosition = new Vector2(- rt.sizeDelta.x / 3 + tmp.textBounds.size.x, (Options.Count - 1) * 15 - 30 * i);
        }

        arr = Instantiate(arrow, Vector3.zero, Quaternion.identity);
        arr.transform.SetParent(optionsBox.transform);
        RectTransform rt2 = arr.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt2.sizeDelta = new Vector2(12, 28);
        rt2.anchoredPosition = new Vector2(- rt.sizeDelta.x / 3 + 5, (Options.Count - 1) * 15);
        rt2.localScale = new Vector3(1,1,1);

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
