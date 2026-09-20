using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public Ingredient[] ingredients;

    // a drag shorter than this counts as a click, not a swipe
    public float minSwipeDistance = 0.3f;

    // where the mouse was when the player pressed down
    Vector2 swipeStart;

    // true while the mouse is being held down
    bool swiping = false;

    void Update()
    {
        // starting to press down
        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = GetMouseWorldPosition();
            swiping = true;
        }

        // stopped pressing down after holding for a while
        if (Input.GetMouseButtonUp(0) && swiping)
        {
            swiping = false;
            Vector2 swipeEnd = GetMouseWorldPosition();

            // ignore this swipe
            if (Vector2.Distance(swipeStart, swipeEnd) >= minSwipeDistance)
            {
                SendSwipe(swipeStart, swipeEnd);
            }
        }
    }


    // let each ingredient check the swipe against its own cut lines
    void SendSwipe(Vector2 start, Vector2 end)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            ingredient.TrySwipe(start, end);
        }
    }


    // gets current position of mouse
    Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
