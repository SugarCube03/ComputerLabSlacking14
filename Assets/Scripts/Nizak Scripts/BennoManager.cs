using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BennoManager : MonoBehaviour
{
    private State currentState;
    [Header("Time Constants")]
    [SerializeField] private float minDistractedTime;
    [SerializeField] private float maxDistractedTime;
    [SerializeField] private float turnBackTime;
    [SerializeField] private float checkTime;

    [SerializeField] private float flashInterval;

    [Header("Components To Attach")]
    [SerializeField] private BennoAnimation bennoAnimation;
    [SerializeField] private GameObject warning;
    private bool warningOn = true;

    [Header("Stress Sound")]
    [SerializeField] private AudioSource stressAudio;
    [SerializeField] private AudioClip stressSound;

    private WaitForSeconds turnWait;
    private WaitForSeconds checkWait;

    private Coroutine flashingCoroutine;

    public enum State
    {
        Teaching,
        Turning,
        Checking
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        turnWait = new WaitForSeconds(turnBackTime);
        checkWait = new WaitForSeconds(checkTime);
        StartCoroutine(BennoBehaviorLoop());
    }

    private IEnumerator BennoBehaviorLoop()
    {
        while (true)
        {
            currentState = State.Turning;
            stressAudio.clip = stressSound;
            stressAudio.loop = true;
            stressAudio.Play();
            flashingCoroutine = StartCoroutine(FlashWarning());
            bennoAnimation.BennoTurn();
            yield return turnWait;

            StopCoroutine(flashingCoroutine);
            warning.SetActive(true);
            stressAudio.loop = false;
            stressAudio.Stop();
            currentState = State.Checking;
            bennoAnimation.BennoCheck();
            yield return checkWait;

            currentState = State.Teaching;
            
            bennoAnimation.BennoTeach();
            warning.SetActive(false);
            yield return new WaitForSeconds(Random.Range(minDistractedTime, maxDistractedTime));

         

           

        

        }
    }

    private IEnumerator FlashWarning()
    {
        if (stressAudio.loop != true)
        {
            stressAudio.loop = true;
            stressAudio.Play();
        }

        warning.SetActive(true);
        while (true)
        {
            warning.SetActive(warningOn);
            yield return new WaitForSeconds(flashInterval);
            warningOn = !warningOn;
        }
    }

    void Update()
    {

    }

    public State CurrentState()
    {
        return currentState;
    }

    public bool IsChecking()
    {
        return currentState == State.Checking;
    }
}
