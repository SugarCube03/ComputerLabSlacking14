using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Fish Stats")]
    public float weight = 1f;    // the weight of fish, affects retract speed
    public int score = 10;       // the score of the fish

    [Header("Movement Settings")]
    public float swimSpeed = 1f;      // swingming speed of the fish
    public float swimRange = 2f;      // swim range of the fish
    public float randomSpeedVariance = 0.3f; //the range of speed variance
    public float randomRangeVariance = 0.5f; // the range of swiming range variance
    private float phaseOffset; // the phase offset of each fish
    private float actualSpeed; //speed of fish
    private float actualRange;//range of fish

    private Vector3 startPosition;
    private float previousOffsetX = 0f;

    void Start()
    {
        startPosition = transform.position* 0.5f;
        transform.localScale*= 0.5f;

        phaseOffset = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
    actualSpeed = swimSpeed + UnityEngine.Random.Range(-randomSpeedVariance, randomSpeedVariance);
    actualRange = swimRange + UnityEngine.Random.Range(-randomRangeVariance, randomRangeVariance);
    actualRange*=0.5f;
    }

    void Update()
    {
        // use a sine wave to make the fish swim back and forth around startPositions
        float offsetX = Mathf.Sin(Time.time * actualSpeed + phaseOffset) * actualRange;
        transform.position = startPosition + new Vector3(offsetX, 0f, 0f);

        // check moving direction and flip sprite accordingly
        if (offsetX > previousOffsetX)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (offsetX < previousOffsetX)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        previousOffsetX = offsetX;
    }
}