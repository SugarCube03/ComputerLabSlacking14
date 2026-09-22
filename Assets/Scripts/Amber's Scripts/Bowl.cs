using UnityEngine;

public class Bowl : MonoBehaviour
{
    // the cutting board
    public CuttingBoard board;
    // counter to track when a salad is finished
    int ingredientsSoFar = 0;
    // how many are needed
    public int ingredientsTotal = 7;

    // the finished salad picture
    public Sprite fullBowl;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // is our salad ready
    public bool IsComplete()
    {
        return ingredientsSoFar >= ingredientsTotal;
    }

    // try to drop a piece in
    public bool TryAdd(DraggableIngredient piece)
    {
        Ingredient ingredient = piece.GetComponent<Ingredient>();

        // not chopped up yet; will bounce back
        if (!ingredient.IsFullyCut())
        {
            return false;
        }

        piece.gameObject.SetActive(false);
        board.Clear();
        ingredientsSoFar++;

        // salad is ready
        if (IsComplete() && fullBowl != null)
        {
            sr.sprite = fullBowl;
        }

        return true;
    }
}
