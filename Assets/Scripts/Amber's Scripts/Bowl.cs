using UnityEngine;

// attach to bowl
public class Bowl : MonoBehaviour
{
    private int currentFill = 0;
    private int capacityFill = 6;

    // checks if bowl is ready (full)
    public bool IsFull()
    {
        return currentFill >= capacityFill;
    }

    // get how many ingredients are in there
    public int GetFill()
    {
        return currentFill;
    }

    // happens when something is dragged in
    public void IncreaseFill()
    {
        currentFill++;
    }
}
