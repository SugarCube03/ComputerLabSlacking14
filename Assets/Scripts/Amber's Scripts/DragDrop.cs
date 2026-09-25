using UnityEngine;
using UnityEngine.InputSystem;

// attach to veggies, dressing, and seasoning; will make them draggable + droppable
public class DragDrop : MonoBehaviour
{
    // for dragging
    // get from input system
    private InputAction clickAction; // will check for mouse being pressed
    private InputAction pointAction; // for mouse's position (point)
    private Collider2D myCollider;
    private bool dragging = false;
    private bool onBoard = false;
    private SpriteRenderer myRenderer;
    private int normalOrder; // the Order in Layer it started with

    // for dropping
    // the two things that it can be dropped on
    [SerializeField] private CuttingBoard cuttingBoard;
    private Collider2D boardCollider;
    [SerializeField] private Bowl bowl;
    private Collider2D bowlCollider;
    private Vector2 startPosition;

    // for cutting
    [SerializeField] private Sprite cutSprite;

    private void Awake()
    {
        clickAction = InputSystem.actions.FindAction("Click");
        pointAction = InputSystem.actions.FindAction("Point");
        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        normalOrder = myRenderer.sortingOrder;
        startPosition = transform.position;
        boardCollider = cuttingBoard.GetComponent<Collider2D>();
        bowlCollider = bowl.GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandleDrag();
        HandleCut();
    }

    private void HandleDrag()
    {
        Vector2 mousePosition = GetMouseWorldPosition();

        // not dragging anymore
        if (dragging && !clickAction.IsPressed())
        {
            dragging = false;
            myRenderer.sortingOrder = normalOrder; // go back to its normal layer
            HandleDrop();
        }

        // just started dragging and we're in the hitbox; AI helped me figure out how to
        // see if the press was happening inside the hitbox
        // also disable dragging if this thing is on the board
        if (clickAction.WasPressedThisFrame() && myCollider.OverlapPoint(mousePosition))
        {
            dragging = true;
            // show on top of everything while dragging
            myRenderer.sortingOrder = 5;
        }

        // still dragging, so have the object follow the mouse
        if (dragging)
        {
            transform.position = mousePosition;
        }
    }

    private void HandleDrop()
    {
        // is ingredient over the cutting board
        if (boardCollider.OverlapPoint(GetMouseWorldPosition()))
        {
            // put ingredient onto cutting board
            if (CompareTag("Cuttable") && !cuttingBoard.IsOccupied())
            {
                transform.position = cuttingBoard.transform.position + new Vector3(0f, 0.5f, 0f); // spot on the cutting board
                cuttingBoard.SetOccupied();
                onBoard = true;
            }
            else
            {
                ReturnToStart();
            }
        }
        // is ingredient over the bowl
        else if (bowlCollider.OverlapPoint(GetMouseWorldPosition()))
        {
            // put ingredient into bowl
            if (CompareTag("NotCuttable"))
            {
                bowl.IncreaseFill();
                gameObject.SetActive(false);
                cuttingBoard.SetNotOccupied();
            }
            else
            {
                ReturnToStart();
            }
        }
        // ingredient was over neither
        else
        {
            ReturnToStart();
        }
    }

    // AI helped me figure how to convert from screen to mouse position
    private Vector2 GetMouseWorldPosition()
    {
        Vector2 screenPosition = pointAction.ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    // return the ingredient back to where it belongs
    private void ReturnToStart()
    {
        transform.position = startPosition;
    }

    // clicking an ingredient that's on the board will cut it
    private void HandleCut()
    {
        Vector2 mousePosition = GetMouseWorldPosition();

        if (clickAction.WasPressedThisFrame() && myCollider.OverlapPoint(mousePosition) && onBoard)
        {
            Cut();
        }
    }

    // "cuts" the thing on the board (replaces sprites)
    private void Cut()
    {
        GetComponent<SpriteRenderer>().sprite = cutSprite;
        tag = "NotCuttable";
        onBoard = false; // make it draggable now
        transform.position = cuttingBoard.transform.position + new Vector3(0f, 0.3f, 0f);
        startPosition = transform.position;
    }
}
