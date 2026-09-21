using UnityEngine;
using UnityEngine.UI;

public class MiniGameButton : MonoBehaviour
{
    [SerializeField] private GameObject minigamePrefab;
    [SerializeField] private minigameLauncher launcher;
    //[SerializeField] private GameManager gameManager;

    private Button button;
    // Update is called once per frame
    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void clicked()
    {
        launcher.OpenGame(minigamePrefab);
    }

    public void disableButton()
    {
        button.interactable= false;
    }
}
