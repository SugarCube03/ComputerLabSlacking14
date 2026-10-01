using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;


//Kylie
//The ShoppingGameManager class manages the entire shopping minigame
//The shopping minigame consists of player input, sounds, a winning condition and a shaking effect

public class ShoppingGameManager : MonoBehaviour, Iminigame
{
    //totalQuestions is the total amount of questions the player answers 
    private int totalQuestions;

    //Reference to the clothing spawner
    [SerializeField] private ClothingSpawner clothingSpawner;

    //currentQuestion keeps count of the question the player is on (aka the item) during the minigame
    private int currentQuestion;

    //Nizak - gamewon keeps track of whether the player has won the game or not
    private bool gamewon = false; //im trying to implement the interface to ur code -nizak

    //playerControls stores and references the input created in the ShopControls Input System 
    private ShopControls playerControls;
    //isShaking checks if the incorrect answer animation for the shake is occuring 
    private bool isShaking;
    //The original position of the ClothingSpawner is stored 
    //This allows the item to return to the original position of the item after shaking 
    Vector3 startPos;
    
    [Header("Shake and tilt")]
    [SerializeField] private bool tilt = true;
    [SerializeField] private bool shake = true;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private float magnitude = 15f;

    [SerializeField] private float angle = 12f;      // max tilt in degrees
    [SerializeField] private float speed = 30f;      // how fast it wobbles left/right

    //Audio Source fields createdto manually add sound effects and background music
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip checkoutSound;
    [SerializeField] private AudioClip discardSound;
    [SerializeField] private AudioClip trashSound;

    //The original rotation of the ClothingSpawner is stored in startRot
    //It is reset after shaking
    Quaternion startRot;

    private void Awake()
    {
        //AI assisted 
        //playerControls creates a new instance of the input controls each time the player
        //checkouts or discards an item
        playerControls = new ShopControls();

        //Both the start position and rotation of the Clothing Spawner is stored in each variable
        startPos = clothingSpawner.transform.position;
        startRot =clothingSpawner.transform.rotation;
        totalQuestions=clothingSpawner.GetSpawnDictionaryLength();
    }


    //AI assisted
    private void OnEnable()
    {
        //The ShoppingController input actions are enabled 
        //After the Checkout input is performed, the OnCheckout function is called 
        //After the Discard input is performed, the OnDiscard function is called
        playerControls.ShoppingController.Enable();
        playerControls.ShoppingController.Checkout.performed += OnCheckout;
        playerControls.ShoppingController.Discard.performed += OnDiscard;

    }
    private void OnDisable()
    {
        //After the GameObject is becomes disabled, the ShoppingController input
        //unsubscribes from the Checkout input event
        playerControls.ShoppingController.Checkout.performed -= OnCheckout;
        playerControls.ShoppingController.Discard.performed -= OnDiscard;
        playerControls.ShoppingController.Disable();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //The StartGame function is called, and starts the minigame
        StartGame();
    }

    //AI assisted 
    private void OnCheckout(InputAction.CallbackContext context)
    {
        //IN OnCheckout, the inputAction is referenced when the player presses right arrow on the keyboard
        //The audioSource plays the checkout sound once
        //SubmitAnswer is called, and denotes the right arrow as true, aka "correct"
        
        SubmitAnswer(true);
      
    }

    
    private void OnDiscard(InputAction.CallbackContext context)
    {
        //In OnDiscard, the inputAction is referenced when the player presses thhe left arrow on the keyboard
        //The audioSource plays the discard sound once
        //SubmitAnswer is called, and denotes the left arrow as false, aka "wrong"
        
        
        SubmitAnswer(false);
    }
   
    public void StartGame()
    {
        //StartGame stores a value in currentQuestion and starts the game from 0
        //NextItem is called, and spawns  next item after the player sorts it
        currentQuestion = 0;

        NextItem();
    }

  
    private void NextItem()

       
        //NextItem moves the game and spawns the next item 
    {

        //Thhe if statement checks if the player answered all the questions
        if (currentQuestion >= totalQuestions)
        {
            //if the player completes all the questions, the game is marked as won
            gamewon = true;
            //The EndGame function is called, in order to end the game after the if statement is checked
            EndGame();
            //This prevents another item from being spawned after the minigame ends
            return;
        }

        //currentQuestion counts the number of questions answer by the player 
        currentQuestion++;

        //the clothingSpawner creates a new random item 
        clothingSpawner.SpawnRandomItem();
    }

  

    //SubmitAnswer checks if the players answer is correct or wrong
    
    private void SubmitAnswer(bool FishingGear)

    {   
        if (isShaking)
        {
            return;
        }
      

        if ( FishingGear == clothingSpawner.CurrentItemIsFishingGear)
        {

            //if FishingGear is the player's answer, it is considered correct
            Debug.Log("Correct!");
            if (FishingGear)
            {
                audioSource.PlayOneShot(checkoutSound);
            }
            else
            {
                audioSource.PlayOneShot(trashSound);
            }
            
            NextItem();
            
        }
        else
        {   //if the player says an item is not fishing gear, then it is false 
            Debug.Log("Incorrect!");
            StartCoroutine(ShakeCoroutine());
            
        }

    }


    //EndGame stops the minigame
    //In addition, it prevents the items spawning from the clothing spawner and
    //destroys an item from the scene
    private void EndGame()
    {
        clothingSpawner.ClearItem();
        playerControls.ShoppingController.Disable();

    }
    
    //Nizak
    public bool IsGameWon()
    {
        return gamewon;
    }

    //Nizak
    public string GetGameInstructions()
    {
        return "Use arrow keys to buy fishing related items and discard others";
    }

    //Nizak
    IEnumerator ShakeCoroutine()
    {
    isShaking = true;
    audioSource.PlayOneShot(discardSound);
     
    
    float elapsed = 0f;

    while (elapsed < duration)
    {
        //ai helped generating the math
         float fade = 1f - elapsed / duration;
        float wave = Mathf.Sin(elapsed * speed) * fade; //sin wave so it smooths

            // left/right shake
            if (shake)
            {
                clothingSpawner.transform.position = startPos + new Vector3(wave * magnitude, 0f, 0f);
            }


            if (tilt)
            {
                clothingSpawner.transform.rotation = startRot * Quaternion.Euler(0f, 0f, wave * angle);

            }
        
        elapsed += Time.deltaTime;
        yield return null;
    }

    // reset both
    clothingSpawner.transform.position = startPos;
    clothingSpawner.transform.rotation = startRot;

    isShaking = false;
}
}
