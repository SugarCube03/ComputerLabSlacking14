using UnityEngine;
using TMPro;

public class FishingController : MonoBehaviour
{
    // showing which state hook currently at
    private enum FishingState { Swinging, Extending, Retracting }
    private FishingState currentState = FishingState.Swinging;

    [Header("References")]
    public Transform hook;              // the hook object
    private LineRenderer lineRenderer;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI fishCountText;
    private int totalScore = 0;
    private int fishCount = 0;

    [Header("Swing Settings")]
    public float swingSpeed = 2f;       // the speed of swinging
    public float maxAngle = 60f;        // the largest angle of swinging

    [Header("Hook Rotation")]
    public float rotationOffset = 90f;

    [Header("Extend and Retract Settings")]
    public float extendSpeed = 5f;      // the speed of extending
    public float maxLength = 5f;        // maximum length of swing

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip castSound;      // sound of casting the fishing rod
    public AudioClip catchSound;     // sound of successfully catch the fish

    private float currentAngle;         // current angle of swing
    private float lockedAngle;          // the angle at the frame when pressing the button
    private float currentLength = 0f;   // the distance between hook and the starting point

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        switch (currentState)
        {
            case FishingState.Swinging:
                HandleSwinging();
                break;
            case FishingState.Extending:
                HandleExtending();
                break;
            case FishingState.Retracting:
                HandleRetracting();
                break;
        }

        UpdateLine();
    }

    void HandleSwinging()
    {
        //  use sine to make sure the angle is between -maxAngle and +maxAngle
        currentAngle = Mathf.Sin(Time.time * swingSpeed) * maxAngle;

        // Use the angle to calculate the direction of hook
        Vector2 dir = AngleToDirection(currentAngle);
        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        hook.rotation = Quaternion.Euler(0, 0, angleDeg + rotationOffset);
        hook.position = transform.position + (Vector3)(dir * 0.1f);

        // press space to lock on current angle, and switch to extending state
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lockedAngle = currentAngle;
            currentLength = 0f;
            currentState = FishingState.Extending;

            audioSource.PlayOneShot(castSound);  
        }
    }

    void HandleExtending()
    {
        currentLength += extendSpeed * Time.deltaTime;

        if (currentLength >= maxLength)
        {
            currentLength = maxLength;
            currentState = FishingState.Retracting; //  when reach the maximum length, start retracting
        }

        Vector2 dir = AngleToDirection(lockedAngle);
        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        hook.rotation = Quaternion.Euler(0, 0, angleDeg + rotationOffset);
        hook.position = transform.position + (Vector3)(dir * currentLength);
    }

    void HandleRetracting()
    {
        float actualRetractSpeed = extendSpeed;
        if (caughtFish != null)
        {
            actualRetractSpeed = extendSpeed / caughtFish.weight;
        }

        currentLength -= actualRetractSpeed * Time.deltaTime;

        if (currentLength <= 0f)
        {
            currentLength = 0f;
            currentState = FishingState.Swinging; // retract to the starting point, start swinging again

            if (caughtFish != null)// if caught the fish, add point and destroy the fish
            {
                totalScore += caughtFish.score;
                fishCount += 1;
                scoreText.text = "Score: " + totalScore;
                fishCountText.text = "Fish Caught: " + fishCount;

                audioSource.PlayOneShot(catchSound); // sound of catching fish

                Destroy(caughtFish.gameObject);
                caughtFish = null;
            }
        }

        Vector2 dir = AngleToDirection(lockedAngle);
        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        hook.rotation = Quaternion.Euler(0, 0, angleDeg + rotationOffset);
        hook.position = transform.position + (Vector3)(dir * currentLength);
    }

    private Fish caughtFish;  // record the caught fish

    public void CatchFish(Fish fish)
    {
        caughtFish = fish;
        currentState = FishingState.Retracting;
    }

    //  change angle to direction
    Vector2 AngleToDirection(float angleDegrees)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(rad), -Mathf.Cos(rad));

    }

    void UpdateLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hook.position);
    }
}