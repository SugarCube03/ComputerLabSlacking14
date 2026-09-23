using UnityEngine;
using UnityEngine.InputSystem;

// attach to veggies, dressing, and seasoning; will make them draggable
public class Drag : MonoBehaviour
{
    // get from input system
    public InputAction clickAction; // will check for mouse being pressed
    public InputAction pointAction; // for mouse's position (point)

    private bool dragging = false;

    private Collider2D itemsCollider;

    private void Awake()
    {
        clickAction = InputSystem.actions.FindAction("Click");
        pointAction = InputSystem.actions.FindAction("Point");
        itemsCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // AI helped me figure how to convert from screen to mouse position
        Vector2 screenPosition = pointAction.ReadValue<Vector2>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // not dragging anymore
        if (!clickAction.IsPressed())
        {
            dragging = false;
        }

        // just started dragging and we're in the hitbox; AI helped me figure out how to
        // see if the press was happening inside the hitbox
        if (clickAction.WasPressedThisFrame() && itemsCollider.OverlapPoint(mousePosition))
        {
            dragging = true;
        }

        // still dragging, so have the object follow the mouse
        if (dragging)
        {
            transform.position = mousePosition;
        }
    }
}
