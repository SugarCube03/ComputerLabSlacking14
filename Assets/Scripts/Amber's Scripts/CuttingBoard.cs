using UnityEngine;

// goes on each ingredient so player can drag it onto the plate
public class CuttingBoard : MonoBehaviour
{
    // where the piece should go when dragged
    public Transform newPiece;

    // to check if there is an ingredient on the board
    public IngredientType currentPiece;

    // put an ingredient on the cutting
    public void Place(DraggableIngredient piece)
    {
        // there is something else there already
        if (currentPiece != null)
        {
            currentPiece.returnHome();
        }

        // snap piece there
        piece.transform.position = newPiece.position;
        currentPiece = piece;
    }

    // get what piece is currently on the cutting board
    public Ingredient GetCurrentIngredient()
    {
        if (currentPiece == null)
        {
            return null;
        }

        return currentPiece.getComponent<Ingredient>();
    }

    // call when the piece leaves the board
    public void Clear()
    {
        currentPiece = null;
    }
}
