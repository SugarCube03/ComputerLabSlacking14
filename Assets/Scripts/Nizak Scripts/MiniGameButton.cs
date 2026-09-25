using UnityEngine;
using UnityEngine.UI;

public class MiniGameButton : MonoBehaviour
{
    [SerializeField] private GameObject minigamePrefab;
    [SerializeField] private minigameLauncher launcher;
 
    [SerializeField] private GameManager gameManager;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void clicked()
    {
        gameManager.SetCurrButton(this);
        launcher.OpenGame(minigamePrefab);

    }

    public void disableButton()
    {
        button.interactable= false;
    }

    public GameObject GetMinigamePrefab()
    {
        return minigamePrefab;
    }
}
