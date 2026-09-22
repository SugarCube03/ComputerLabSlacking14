using UnityEngine;

// for the cuttingboard
public class CuttingBoard : MonoBehaviour
{
    // where the piece should go when dragged
    public Transform slot;

    // to check if there is an ingredient on the board
    public DraggableIngredient currentPiece;

    // put an ingredient on the cutting
    public void Place(DraggableIngredient piece)
    {
        // there is something else there already
        if (currentPiece != null)
        {
            currentPiece.ReturnHome();
        }

        // snap piece there
        piece.transform.position = slot.position;
        currentPiece = piece;
    }

    // get what piece is currently on the cutting board
    public Ingredient GetCurrentIngredient()
    {
        if (currentPiece == null)
        {
            return null;
        }

        return currentPiece.GetComponent<Ingredient>();
    }

    // call when the piece leaves the board
    public void Clear()
    {
        currentPiece = null;
    }
}
