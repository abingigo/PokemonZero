using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonsPage : MonoBehaviour
{
    int currpoke;
    PlayerMovement pm;

    public void Setup(int poke)
    {
        currpoke = poke;
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject g = GameObject.FindObjectOfType<Summary>().GetScreen(3);
            g.SetActive(true);
            g.GetComponent<MovesPage>().Setup(currpoke);
            gameObject.SetActive(false);
            pm.gameObject.GetComponent<AudioSource>().clip = AudioClips.summaryPage;
            pm.gameObject.GetComponent<AudioSource>().Play();
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currpoke != PlayerProfile.party_count - 1)
            {
                currpoke++;
                Setup(currpoke);
                FindObjectOfType<Summary>().Setup(currpoke);
            }
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currpoke != 0)
            {
                currpoke--;
                Setup(currpoke);
                FindObjectOfType<Summary>().Setup(currpoke);
            }
        }
    }
}
