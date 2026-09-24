using UnityEngine;

// "boss" script for the salad minigame
public class SaladManager : MonoBehaviour, Iminigame
{
    [SerializeField] private Bowl bowl;

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
