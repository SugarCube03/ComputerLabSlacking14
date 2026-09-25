using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;



public class GameManager : MonoBehaviour
{
    [SerializeField] private minigameLauncher launcher;
   [SerializeField] private GameObject clear;

   [SerializeField] private BennoManager bennoManager;
   [SerializeField] private MiniGameButton [] minigameButtonList;

   [SerializeField] private float timeLimit = 60f;
   [SerializeField] private TimerCode timer;

    private MiniGameButton currButton;

    private HashSet<MiniGameButton> clearedGames = new HashSet<MiniGameButton>();

    private bool currentlySlacking;

 
    private float timeRemaining;
    
    public enum EndReason
    {
        Won,
        Caught,
        TimedOut
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentlySlacking = false;
        clear.SetActive(false);
        timeRemaining = timeLimit;
        StartCoroutine(CountdownTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (clearedGames.Count == minigameButtonList.Length)
        {
            EndGame(EndReason.Won);
        }

      currentlySlacking = launcher.GetMinigameStatus();

      if (currentlySlacking)
        {
            if (!bennoManager.IsChecking())
            {
                if (launcher.IsMinigameCleared()){
                clear.SetActive(true);
                currButton.disableButton();
                clearedGames.Add(currButton);}
            }
            else
            {
                EndGame(EndReason.Caught);
            }
        }
           
        else
        {
            clear.SetActive(false);
        }
    
    }

    void EndGame( EndReason reason)
    {
        StopAllCoroutines();
        bennoManager.StopAllCoroutines();
        Debug.Log(reason);
        launcher.CloseGame();
    }

    public void SetCurrButton(MiniGameButton button)
    {
        currButton = button;
    }
    private IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            timer.SetTime(timeRemaining);

            yield return null;
        }

        EndGame(EndReason.TimedOut);
    }


    
}
