using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;


public class ShoppingGameManager : MonoBehaviour, Iminigame
{
    [SerializeField] private int totalQuestions = 10;

    [SerializeField] private ClothingSpawner clothingSpawner;

    private int currentQuestion;

    private bool gamewon = false; //im trying to implement the interface to ur code -nizak

    private ShopControls playerControls;
    private bool isShaking;
    Vector3 startPos;
    
    [Header("Shake and tilt")]
    [SerializeField] private bool tilt = true;
    [SerializeField] private bool shake = true;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private float magnitude = 15f;

     [SerializeField] private float angle = 12f;      // max tilt in degrees
    [SerializeField] private float speed = 30f;      // how fast it wobbles left/right

    Quaternion startRot ;

   
   

 
    private void Awake()
    {
        playerControls = new ShopControls();
        startPos = clothingSpawner.transform.position;
        startRot =clothingSpawner.transform.rotation;
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
        if (isShaking)
        {
            return;
        }
      

        if ( FishingGear == clothingSpawner.CurrentItemIsFishingGear)
        {
            Debug.Log("Correct!");
            NextItem();
        }
        else
        {
            Debug.Log("Incorrect!");
            StartCoroutine(ShakeCoroutine());
        }

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

    IEnumerator ShakeCoroutine()
    {
    isShaking = true;
     
    
    float elapsed = 0f;

    while (elapsed < duration)
    {
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
