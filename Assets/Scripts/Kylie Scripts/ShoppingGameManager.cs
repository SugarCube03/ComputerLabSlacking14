using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;


public class ShoppingGameManager : MonoBehaviour, Iminigame
{
    [SerializeField] private int totalQuestions = 10;

    [SerializeField] private ClothingSpawner clothingSpawner;

    private int currentQuestion;
    private int playerScore;
    private bool gamewon = false; //im trying to implement the interface to ur code -nizak

    private ShopControls playerControls;

 
    private void Awake()
    {
        playerControls = new ShopControls();
    }


    private void OnEnable()
    {
        playerControls.ShoppingController.Enable();
        playerControls.ShoppingController.Checkout.performed += OnCheckout;
        playerControls.ShoppingController.Discard.performed += OnDiscard;

    }
    private void OnDisable()
    {
        playerControls.ShoppingController.Checkout.performed -= OnCheckout;
        playerControls.ShoppingController.Discard.performed -= OnDiscard;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    private void OnCheckout(InputAction.CallbackContext context)
    {
        SubmitAnswer(true);
    }

    private void OnDiscard(InputAction.CallbackContext context)
    {
        SubmitAnswer(false);
    }
   
    public void StartGame()
    {
        currentQuestion = 0;
        playerScore = 0;

        NextItem();
    }


    private void NextItem()
    {
        if (currentQuestion >= totalQuestions)
        {
            gamewon = true;
            EndGame();
            return;
        }

        currentQuestion++;

        clothingSpawner.SpawnRandomItem();
    }


    private void SubmitAnswer(bool FishingGear)
    {
        bool correct = FishingGear == clothingSpawner.CurrentItemIsFishingGear;

        if (correct)
        {
            playerScore++;
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }

        Debug.Log("Score: " + playerScore +  "/" + currentQuestion);
        NextItem();
    }

    private void EndGame()
    {
        clothingSpawner.ClearItem();

    }

    public bool IsGameWon()
    {
        return gamewon;
    }

    public string GetGameInstructions()
    {
        throw new System.NotImplementedException();
    }
}
