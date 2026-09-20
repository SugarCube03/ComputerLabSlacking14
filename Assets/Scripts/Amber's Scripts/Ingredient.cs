using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // what is this ingredient
    public IngredientType type;

    // pictures of ingredient at diff cuts
    public Sprite[] cutSprites;

    // picture we swap out as cuts land
    SpriteRenderer sr;

    // cut lines belonging to this ingredient
    CutLine[] cutLines;

    // how many cuts have been done so far
    int cutsDone = 0;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        // grab cut lines underneath this ingredient
        cutLines = GetComponentsInChildren<CutLine>();
    }

    // is every line on this ingredient cut
    public bool IsFullyCut()
    {
        return cutsDone >= cutLines.Length;
    }

    // called by the knife when the player swipes
    public bool TrySwipe(Vector2 swipeStart, Vector2 swipeEnd)
    {
        // already fully cut
        if (IsFullyCut())
        {
            return false;
        }

        // only the next line in order can be cut
        CutLine nextLine = cutLines[cutsDone];

        if (nextLine.TryCut(swipeStart, swipeEnd))
        {
            ApplyCut();
            return true;
        }

        return false;
    }

    // swap to the next picture now that the cut has landed
    void ApplyCut()
    {
        if (cutsDone < cutSprites.Length)
        {
            sr.sprite = cutSprites[cutsDone];
        }

        cutsDone++;
    }
}
