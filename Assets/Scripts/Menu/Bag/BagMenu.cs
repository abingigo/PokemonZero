using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagMenu : MonoBehaviour
{
    [SerializeField] GameObject[] screens = new GameObject[8];
    [SerializeField] TextMeshProUGUI[] items = new TextMeshProUGUI[7];
    [SerializeField] TextMeshProUGUI[] counts = new TextMeshProUGUI[7];
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemText;
    [SerializeField] Image cursor;
    [SerializeField] GameObject scroller;
    [SerializeField] Image scrollBar;
    int currentPage, currentItemNo, lastElementVisible;
    List<InventoryItem> currentItems;
    bool selected = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Cancel();
        if(Input.GetKeyDown(KeyCode.A))
        {
            screens[currentPage].gameObject.SetActive(false);
            currentPage--;
            if (currentPage < 0)
                currentPage = 7;
            Setup(currentPage);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            screens[currentPage].gameObject.SetActive(false);
            currentPage++;
            if(currentPage > 7)
                currentPage = 0;
            Setup(currentPage);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            currentItemNo--;
            if (currentItemNo < 0)
            {
                currentItemNo = currentItems.Count;
                cursor.rectTransform.anchoredPosition -= new Vector2(0, 34f * lastElementVisible);
                displayItems(currentItemNo - 6);
                lastElementVisible = currentItemNo;
            }
            else if(currentItemNo <= 4 && lastElementVisible > 6)
            {
                lastElementVisible--;
                displayItems(lastElementVisible - 6);
            }
            else
                cursor.rectTransform.anchoredPosition += new Vector2(0, 34f);
            viewDetails(currentItemNo);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentItemNo++;
            if (currentItemNo > currentItems.Count)
            {
                currentItemNo = 0;
                cursor.rectTransform.anchoredPosition = new Vector2(70f, 144f);
                if (currentItems.Count + 1 < 7)
                    lastElementVisible = currentItems.Count;
                else
                    lastElementVisible = 6;
                displayItems(currentItemNo);
            }
            else if (currentItemNo >= 4 && lastElementVisible < currentItems.Count)
            {
                lastElementVisible++;
                displayItems(lastElementVisible - 6);
            }
            else
                cursor.rectTransform.anchoredPosition -= new Vector2(0, 34f);
            viewDetails(currentItemNo);
        }
        if(Input.GetKeyDown(KeyCode.E))
            Select(currentItemNo);
    }

    public void Setup(int pageNumber)
    {
        currentPage = pageNumber;
        currentItems = new List<InventoryItem>();
        screens[pageNumber].SetActive(true);
        foreach (InventoryItem i in PlayerProfile.items)
            if (i.items.pocket - 1 == pageNumber)
                currentItems.Add(i);
        currentItemNo = 0;
        cursor.rectTransform.anchoredPosition = new Vector2(70f, 144f);
        if (currentItems.Count <= 6)
        {
            scroller.SetActive(false);
            lastElementVisible = currentItems.Count;
        }
        else
        {
            lastElementVisible = 6;
            scroller.SetActive(true);
            scrollBar.rectTransform.sizeDelta = new Vector2(36, 178 / (currentItems.Count - 5));
            //-36 and 142 mid 53 len 178
            scrollBar.rectTransform.anchoredPosition = new Vector2(232, (142 - 36) * ((currentItems.Count - 5) * 2 - 1) / ((currentItems.Count - 5) * 2));
        }
        displayItems(currentItemNo);
        viewDetails(currentItemNo);
    }

    void displayItems(int displayFrom)
    {
        int numOnPage = 0;
        if (displayFrom < 0)
            displayFrom = 0;
        int i = displayFrom;
        while(i < currentItems.Count && i - displayFrom < 7)
        {
            items[numOnPage].text = currentItems[i].items.Name;
            counts[numOnPage].text = "x" + currentItems[i].count.ToString();
            i++;
            numOnPage++;
        }
        if (i - displayFrom <= 6)
        {
            items[numOnPage].text = "CLOSE BAG";
            counts[numOnPage].text = "";
            numOnPage++;
        }
        while(numOnPage < 7)
        {
            items[numOnPage].text = "";
            counts[numOnPage++].text = "";
        }
    }

    void viewDetails(int itemNo)
    {
        if (itemNo == currentItems.Count)
        {
            itemImage.sprite = (Sprite)Resources.Load("Graphics/Items/back", typeof(Sprite));
            itemText.text = "Close bag.";
            return;
        }
        itemImage.sprite = (Sprite)Resources.Load($"Graphics/Items/{currentItems[itemNo].items.Name.ToUpper().Replace(" ", "")}", typeof(Sprite));
        itemText.text = currentItems[itemNo].items.description;
    }

    void Select(int currentItem)
    {
        if (currentItem == currentItems.Count)
            Cancel();
        selected = true;
        OptionsBox ob;
        ob = OptionsBox.Instance;

    }
    void Cancel()
    {
        gameObject.SetActive(false);
        FindObjectOfType<Menu>().ShowMenu();
    }
}
