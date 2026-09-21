using TMPro;
using UnityEngine;

// brain of minigame
public class SandwichManager : MonoBehaviour
{
    // three parts of playthrough
    public enum Phase
    {
        Prep,
        Assemble,
        Done
    }

    // the ingredients
    public Ingredient[] ingredients;
    // every draggable piece
    public DraggableIngredient[] pieces;
    // the knife
    public KnifeController knife;
    // the plate
    public SandwichStack stack;

    // seconds the player gets
    public float timeLimit = 60f;
    // how much time we have left
    float timeLeft;

    // countdown on screen
    public TMP_Text timerText;

    // is the sandwich done
    public bool sandwichDone = false;

    // check if game is done and if we won
    public bool isFinished = false;
    public bool didWin = false;

    // start with prep phase
    Phase current = Phase.Prep;

    void Start()
    {
        timeLeft = timeLimit;
        StartPrep();
    }

    void Update()
    {
        // player already left
        if (isFinished)
        {
            return;
        }

        // time changing
        timeLeft -= Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
        }

        // got caught: here after time ran out
        if (timeLeft <= 0f)
        {
            GetCaught();
            return;
        }

        // everything is cut, move on to stacking
        if (current == Phase.Prep && AllCut())
        {
            StartAssemble();
        }
        // sandwich is built, nothing left to do but leave
        else if (current == Phase.Assemble && stack.IsComplete())
        {
            FinishSandwich();
        }
    }

    // have all the ingredients been cut up
    bool AllCut()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            if (!ingredient.IsFullyCut())
            {
                return false;
            }
        }

        return true;
    }

    // cutting phase: knife on, dragging off
    void StartPrep()
    {
        current = Phase.Prep;
        knife.enabled = true;

        foreach (DraggableIngredient piece in pieces)
        {
            piece.enabled = false;
        }
    }

    // stacking phase: knife off, dragging on
    void StartAssemble()
    {
        current = Phase.Assemble;
        knife.enabled = false;

        foreach (DraggableIngredient piece in pieces)
        {
            piece.enabled = true;
        }
    }

    // sandwich is built
    void FinishSandwich()
    {
        current = Phase.Done;
        sandwichDone = true;
        StopPlaying();
    }

    // player exits minigame
    public void CloseMinigame()
    {
        isFinished = true;
        didWin = sandwichDone;
        StopPlaying();
    }

    // time ran out while they were still in here
    void GetCaught()
    {
        isFinished = true;
        didWin = false;
        StopPlaying();
    }

    // stop the player interacting with anything
    void StopPlaying()
    {
        knife.enabled = false;
        foreach (DraggableIngredient piece in pieces)
        {
            piece.enabled = false;
        }
    }
}
