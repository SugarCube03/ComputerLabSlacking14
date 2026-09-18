using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Fish Stats")]
    public float weight = 1f;    // the weight of fish, affects retract speed
    public int score = 10;       // the score of the fish

    [Header("Movement Settings")]
    public float swimSpeed = 1f;      // swingming speed of the fish
    public float swimRange = 2f;      // swim range of the fish

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // use a sine wave to make the fish swim back and forth around startPosition
        float offsetX = Mathf.Sin(Time.time * swimSpeed) * swimRange;
        transform.position = startPosition + new Vector3(offsetX, 0f, 0f);
    }
}