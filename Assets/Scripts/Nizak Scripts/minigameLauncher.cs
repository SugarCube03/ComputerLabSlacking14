using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class minigameLauncher : MonoBehaviour
{
   
    [SerializeField] private Transform minigameContainer;
    [SerializeField] private GameObject minigameSetupUI;
    [SerializeField] Button exitButton;
    private GameObject currGamePrefab;
    private Iminigame currMinigame;

    private bool minigameOpen;

    void Awake()
    {
        minigameSetupUI.SetActive(false);
        minigameOpen = false;
    }
    public void OpenGame(GameObject minigamePrefab)
    {
        minigameOpen = true;
        minigameSetupUI.SetActive(true);
        currGamePrefab = Instantiate(minigamePrefab, minigameContainer.position, Quaternion.identity, minigameContainer);
        currMinigame = currGamePrefab.GetComponentInChildren<Iminigame>();

    }

    public void CloseGame()
    {
        minigameSetupUI.SetActive(false);
        Destroy(currGamePrefab);
        minigameOpen = false;
    }
 
    public bool IsMinigameCleared()
    {
        return currMinigame.IsGameWon();
    }

    public bool GetMinigameStatus()
    {
        return minigameOpen;
    }
   

}
