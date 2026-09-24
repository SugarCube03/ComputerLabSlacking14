using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class FishingController : MonoBehaviour, Iminigame
{
    // showing which state hook currently at
    private enum FishingState { Swinging, Extending, Retracting }
    private FishingState currentState = FishingState.Swinging;
    private String GameInstruction = "Press space to catch the fish";//game instruction

    [Header("References")]
    public Transform hook;              // the hook object
    private LineRenderer lineRenderer;

    [Header("UI")]
    //public TextMeshProUGUI scoreText; //put the score text here
    //public TextMeshProUGUI fishCountText;// put the fish Count text here
    private int totalScore = 0;// start with 0
    private int fishCount = 0;

    [Header("Fish Icons")]
    public Image[] fishIcons;        // 5 Images
    public Sprite caughtSprite; 
    public Sprite uncaughtSprite; 

    [Header("Ending")]
    public GameObject endingPanel;

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
        lineRenderer.positionCount = 2; //line renderer use two points/positions to draw a line

        if (fishIcons != null)
        {
            foreach (Image icon in fishIcons)
            {
                icon.sprite = uncaughtSprite;
            }
        }
    }

    void Update()
    {
        switch (currentState)//there are 3 different type of state, and the switch will moniter which state is currently on
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
        //  use sine to make sure the angle is between -maxAngle and +maxAngle, time.time is the the total time of this round of game play
        currentAngle = Mathf.Sin(Time.time * swingSpeed) * maxAngle;

        // Use the angle to calculate the direction of hook
        Vector2 dir = AngleToDirection(currentAngle);
        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;// Atan2 is using the value of x and y to deduct current degree, and Mathf.Rad2Deg is switch from radian to degree
        hook.rotation = Quaternion.Euler(0, 0, angleDeg + rotationOffset);// this is for setting the hook rotation, only change the z axis. because the hook doesn't rotate in the angle that I expected, so I add this to fix the problem
        hook.position = transform.position + (Vector3)(dir * 0.1f);//set the position of hook, to make sure the hook is connected to the end of the fishing rod
        // press space to lock on current angle, and switch to extending state
        if (Input.GetMouseButtonDown(0))
        {
            lockedAngle = currentAngle;// freeze the angle of hook
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
            actualRetractSpeed = extendSpeed / caughtFish.weight;// heavier the fish, longer the time to catch the fish
        }

        currentLength -= actualRetractSpeed * Time.deltaTime;//the speed of retracting length

        if (currentLength <= 0f)
        {
            currentLength = 0f;
            currentState = FishingState.Swinging; // retract to the starting point, start swinging again

            if (caughtFish != null)
            {
                totalScore += caughtFish.score;
                fishCount += 1;
                //scoreText.text = "Score: " + totalScore;
                //fishCountText.text = "Fish Caught: " + fishCount;

                audioSource.PlayOneShot(catchSound);

                // turn the icons yellow by index
                if (fishIcons != null && fishCount - 1 < fishIcons.Length)
                {
                    fishIcons[fishCount - 1].sprite = caughtSprite;
                }

                Destroy(caughtFish.gameObject);
                caughtFish = null;

                // check whether all fish is caught
                if (IsGameWon())
                {
                    ShowEnding();
                }
            }
        }

        Vector2 dir = AngleToDirection(lockedAngle);
        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        hook.rotation = Quaternion.Euler(0, 0, angleDeg + rotationOffset);
        hook.position = transform.position + (Vector3)(dir * currentLength);
    }

    private Fish caughtFish;  // record the caught fish
    public bool HasCaughtFish => caughtFish != null;

    public void CatchFish(Fish fish)
    {
    if (caughtFish != null)
    {
        return; 
    }

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

    void ShowEnding()
    {
        if (endingPanel != null)
        {
            endingPanel.SetActive(true);
        }
    }

    public bool IsGameWon()
    {
        return fishCount == 5;
    }

    public string GetGameInstructions()
    {
        return GameInstruction;
    }
}