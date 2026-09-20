using UnityEngine;

public class CutLine : MonoBehaviour
{
    // line length
    public float length = 1f;
    // how sloppy the player can be
    public float tolerance = 0.5f;

    // has this line been cut yet
    public bool isCut = false;

    // where the line starts
    public Vector2 GetStartPoint()
    {
        Vector2 center = transform.position;
        // along x axis
        Vector2 direction = transform.right;
        // go left
        return center - (direction * (length / 2f));
    }

    // where the line ends
    public Vector2 GetEndPoint()
    {
        Vector2 center = transform.position;
        // along x axis
        Vector2 direction = transform.right;
        // go right
        return center + (direction * (length / 2f));
    }

    // true if the player cut along this line
    public bool TryCut(Vector2 swipeStart, Vector2 swipeEnd)
    {
        // already cut
        if (isCut)
        {
            return false;
        }

        // need to cut from lineStart to lineEnd
        Vector2 lineStart = GetStartPoint();
        Vector2 lineEnd = GetEndPoint();

        // allows player to cut either way
        float forwardError = Vector2.Distance(swipeStart, lineStart) + Vector2.Distance(swipeEnd, lineEnd);
        float backwardError = Vector2.Distance(swipeStart, lineEnd) + Vector2.Distance(swipeEnd, lineStart);

        // check which direction they actually cut
        float bestError = Mathf.Min(forwardError, backwardError);

        // average how far off each end was
        float averageError = bestError / 2f;

        // too far from line, no cut
        if (averageError > tolerance)
        {
            return false;
        }

        isCut = true;
        return true;
    }

    // draws a line 
    void OnDrawGizmos()
    {
        if (isCut)
        {
            Gizmos.color = Color.gray;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawLine(GetStartPoint(), GetEndPoint());
    }
}
