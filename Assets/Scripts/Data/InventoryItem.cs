[System.Serializable]
public class InventoryItem
{
    public Items items;
    public int count;

    public InventoryItem(Items item)
    {
        items = item;
        count = 1;
    }

    public void increaseByAmount(int a)
    {
        count += a;
    }
}
