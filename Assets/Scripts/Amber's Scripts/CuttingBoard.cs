using UnityEngine;

// attach to cutting board
public class CuttingBoard : MonoBehaviour
{
    private bool boardOccupied = false;

    // check board's status
    public bool IsOccupied()
    {
        return boardOccupied;
    }

    // board has ingredient on it
    public void SetOccupied()
    {
        boardOccupied = true;
    }

    // board does not have ingredient on it
    public void SetNotOccupied()
    {
        boardOccupied = false;
    }
}
