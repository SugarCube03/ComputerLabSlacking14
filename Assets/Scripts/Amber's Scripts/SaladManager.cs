using UnityEngine;

// "boss" script for the salad minigame
public class SaladManager : MonoBehaviour, Iminigame
{
    [SerializeField] private Bowl bowl;
    [SerializeField] private GameObject choppingStep;
    [SerializeField] private GameObject dressingStep;
    [SerializeField] private Vector3 newBowlPosition;
    private bool onDressingStep = false;

    // start with chopping step
    private void Start()
    {
        choppingStep.SetActive(true);
        dressingStep.SetActive(false);
    }

    private void Update()
    {
        // everything has been cut and placed in the bowl
        if (!onDressingStep && bowl.GetFill() >= 4)
        {
            onDressingStep = true;
            choppingStep.SetActive(false);
            dressingStep.SetActive(true);
            // move the bowl to the center
            bowl.transform.position = newBowlPosition;
        }
    }

    // the salad game is won when the bowl is full
    public bool IsGameWon()
    {
        return bowl.IsFull();
    }

    // what the player should do
    public string GetGameInstructions()
    {
        return "Drag the veggies onto the cutting board and click to cut them. Then drag everything into the bowl!";
    }
}
