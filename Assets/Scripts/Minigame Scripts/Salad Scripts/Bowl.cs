using UnityEngine;

// attach to bowl
public class Bowl : MonoBehaviour
{
    private int currentFill = 0;
    private int capacityFill = 6;

    // check if bowl has all the ingredients
    public bool IsFull()
    {
        return currentFill >= capacityFill;
    }

    // get how many ingredients are in the bowl
    public int GetFill()
    {
        return currentFill;
    }

    // increase how many ingredients are in the bowl
    public void IncreaseFill()
    {
        currentFill++;
    }
}
