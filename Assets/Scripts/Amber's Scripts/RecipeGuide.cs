using TMPro;
using UnityEngine;

public class RecipeGuide : MonoBehaviour
{
    // one text label per step of the recipe
    public TMP_Text[] stepLabels;

    // one checkmark image per step
    public GameObject[] checkmarks;

    // fill in the labels and hide all the checkmarks by turning them off
    public void Build(IngredientType[] recipe)
    {
        for (int i = 0; i < recipe.Length; i++)
        {
            stepLabels[i].text = recipe[i].ToString();
            checkmarks[i].SetActive(false);
        }
    }

    // tick off one step once the player stacks it
    public void MarkDone(int stepIndex)
    {
        checkmarks[stepIndex].SetActive(true);
    }
}
