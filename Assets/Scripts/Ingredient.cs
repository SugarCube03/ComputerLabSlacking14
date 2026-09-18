using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum IngredientType
    {
        Bread,
        Mayo,
        Fish,
        Lettuce,
        Tomato
    }

    // what is this ingredient
    public IngredientType type;

    // pictures
    public Sprite[] cutSprites;

    SpriteRenderer sr;
    int cutsDone = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (cutsDone < cutSprites.Length)
        {
            sr.sprite = cutSprites[cutsDone];
            cutsDone++;
        }
    }

}
