using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class minigameLauncher : MonoBehaviour
{
   
    [SerializeField] private Transform minigameContainer;
    [SerializeField] private GameObject minigameSetupUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] Button exitButton;

    private GameObject currGame;
    private bool cleared;

    void Awake()
    {
        minigameSetupUI.SetActive(false);
    }
    public void OpenGame(GameObject minigamePrefab)
    {
        minigameSetupUI.SetActive(true);
        currGame = Instantiate(minigamePrefab, minigameContainer.position, Quaternion.identity, minigameContainer);
        Debug.Log("opening minigame");
    }

    public void CloseGame()
    {
        minigameSetupUI.SetActive(false);
        Destroy(currGame);
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
