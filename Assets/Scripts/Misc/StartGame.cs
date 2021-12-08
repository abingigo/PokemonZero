using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Sprite boy;
    public Sprite girl;
    public RuntimeAnimatorController boy_anim;
    public RuntimeAnimatorController girl_anim;
    public GameObject player;
    public GameObject BGM;
    public AudioClip ac;

    private void Start()
    {
        PlayerProfile pp = GameObject.FindObjectOfType<PlayerProfile>();
        if(pp.gender == 0)
        {
            player.GetComponent<SpriteRenderer>().sprite = boy;
            player.GetComponent<Animator>().runtimeAnimatorController = boy_anim;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sprite = girl;
            player.GetComponent<Animator>().runtimeAnimatorController = girl_anim;
        }

        BGM.GetComponent<AudioSource>().clip = ac;
        BGM.GetComponent<AudioSource>().Play();
    }
}
