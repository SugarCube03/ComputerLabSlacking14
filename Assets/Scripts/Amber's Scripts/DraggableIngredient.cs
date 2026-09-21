using UnityEngine;

// goes on each ingredient so player can drag it onto the plate
public class DraggableIngredient : MonoBehaviour
{
    // what type of piece is this
    public IngredientType type;

    // the plate we're dropping onto
    public SandwichStack stack;

    // how close to the plate the drop has to be to count
    public float snapDistance = 1f;

    // where the piece started
    Vector3 startPosition;

    // true while the player is holding this piece
    bool dragging = false;

    void Start()
    {
        startPosition = transform.position;
    }

    // called whe mouse is pressed on object's collider
    void OnMouseDown()
    {
        dragging = true;
    }

    // called when mouse is held down on this object
    void OnMouseDrag()
    {
        if (dragging)
        {
            transform.position = GetMouseWorldPosition();
        }
    }

    // called when the player lets go
    void OnMouseUp()
    {
        dragging = false;

        // dropped too far from the plate
        if (Vector3.Distance(transform.position, stack.transform.position) > snapDistance)
        {
            ReturnHome();
            return;
        }

        // right place, but wrong ingredient for this step
        if (!stack.TryPlace(this))
        {
            ReturnHome();
        }
    }

    // snap back to where it started
    void ReturnHome()
    {
        transform.position = startPosition;
    }

    // gets current position of mouse
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}
