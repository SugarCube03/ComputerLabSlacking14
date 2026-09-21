using UnityEngine;

public class SandwichStack : MonoBehaviour
{

    // correct order to build in
    public IngredientType[] recipe;

    // where the next layer goes
    public Transform nextLayer;

    // vertical gap between each layer
    public float layerHeight = 0.2f;

    // guide card on the side
    public RecipeGuide guide;

    // how many layers are down so far/index of next ingredient
    int placedCount = 0;

    void Start()
    {
        if (guide != null)
        {
            guide.Build(recipe);
        }
    }

    // is the whole sandwich complete
    public bool IsComplete()
    {
        return placedCount >= recipe.Length;
    }

    // try to add a piece to the stack; has to be in the correct order
    public bool TryPlace(DraggableIngredient piece)
    {
        // sandwich is done
        if (IsComplete())
        {
            return false;
        }

        // the only ingredient allowed right now
        IngredientType expected = recipe[placedCount];

        // wrong one
        if (piece.type != expected)
        {
            return false;
        }

        // put the piece where the next layer goes
        piece.transform.position = nextLayer.position;
        // lock it down
        piece.enabled = false;

        // draw each layer on top of the one below it
        piece.GetComponent<SpriteRenderer>().sortingOrder = placedCount;

        // move up the nextLayer
        Vector3 temp = nextLayer.position;
        temp.y += layerHeight;
        nextLayer.position = temp;

        // tick it off on the guide card
        if (guide != null)
        {
            guide.MarkDone(placedCount);
        }

        placedCount++;
        return true;
    }
}
