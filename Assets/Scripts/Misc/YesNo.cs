using UnityEngine;

public class YesNo : MonoBehaviour
{
    public int i = 0;
    public bool answered = false;
    [SerializeField] GameObject arrow;

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && i == 1)
        {
            arrow.GetComponent<RectTransform>().position += new Vector3(0, 35, 0);
            i--;
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && i == 0)
        {
            arrow.GetComponent<RectTransform>().position -= new Vector3(0, 35, 0);
            i++;
        }
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)))
            answered = true;
    }
}
