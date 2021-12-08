using UnityEngine;

public class GeneralSprites : MonoBehaviour
{
    public Sprite genderMale, genderFemale;
    [SerializeField] Texture2D types_0;
    [HideInInspector] public Sprite normal, fighting, flying, poison, ground, rock, bug, ghost, steel, Null, fire, water, grass, electric, psychic, ice, dragon, dark, fairy;

    private void Start()
    {
        normal   =  Sprite.Create(types_0, new Rect(0, types_0.height * 18 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        fighting =  Sprite.Create(types_0, new Rect(0, types_0.height * 17 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        flying   =  Sprite.Create(types_0, new Rect(0, types_0.height * 16 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        poison   =  Sprite.Create(types_0, new Rect(0, types_0.height * 15 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        ground   =  Sprite.Create(types_0, new Rect(0, types_0.height * 14 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        rock     =  Sprite.Create(types_0, new Rect(0, types_0.height * 13 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        bug      =  Sprite.Create(types_0, new Rect(0, types_0.height * 12 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        ghost    =  Sprite.Create(types_0, new Rect(0, types_0.height * 11 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        steel    =  Sprite.Create(types_0, new Rect(0, types_0.height * 10 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        Null     =  Sprite.Create(types_0, new Rect(0, types_0.height * 9 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        fire     =  Sprite.Create(types_0, new Rect(0, types_0.height * 8 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        water    =  Sprite.Create(types_0, new Rect(0, types_0.height * 7 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        grass    =  Sprite.Create(types_0, new Rect(0, types_0.height * 6 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        electric =  Sprite.Create(types_0, new Rect(0, types_0.height * 5 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        psychic  =  Sprite.Create(types_0, new Rect(0, types_0.height * 4 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        ice      =  Sprite.Create(types_0, new Rect(0, types_0.height * 3 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        dragon   =  Sprite.Create(types_0, new Rect(0, types_0.height * 2 / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        dark     =  Sprite.Create(types_0, new Rect(0, types_0.height / 19, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
        fairy    =  Sprite.Create(types_0, new Rect(0, 0, types_0.width, types_0.height / 19), new Vector2(0, 0), .01f);
    }
}
