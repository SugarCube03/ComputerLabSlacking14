using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

//Kylie 
//The ClothingSpawner class spawns random items and displays the items on the screen 
//It selects a random item from the list based on the boolean created to check if items
//are fishing gear or non-fishing gear

public class ClothingSpawner : MonoBehaviour
{
    //Both arrays store fishing and non fishing gear as prefabs
    //The canvas is a reference for where the items should spawn in the main scene
    [SerializeField] private GameObject[] FishingGear;
    [SerializeField] private GameObject[] NonFishingGear;
    [SerializeField] private Canvas canvas;


    //Incorporation of rng for both fishing gear and non fishing gear 

    Dictionary <GameObject, bool> shopStack = new Dictionary<GameObject, bool>();

    [SerializeField] private float buyChance = 0.5f;
    

    private int canvasLayer;

    //This GameObject stores the asset of the current item that is spawned
    private GameObject currentItem;

    //Type bool that will check whether the currently spawned item is fishing gear
    private bool currentItemIsFishingGear;
    public bool CurrentItemIsFishingGear => currentItemIsFishingGear;

    int randomIndex;

    //AI assisted - SpawnRandomItem allows other scripts to check
    //if the current item is fishing gear or non-fishing gear

    public void SpawnRandomItem()
    {
        if(currentItem != null)
        {
            Destroy(currentItem);
        } 
        
            bool isBuy = UnityEngine.Random.value < buyChance; 
            GameObject[] spawnList = isBuy ? FishingGear : NonFishingGear;

            
            randomIndex = UnityEngine.Random.Range(0, spawnList.Length);
        

         GameObject selectedItem =spawnList[randomIndex];

        //i made ot so that the prefabs u make are children of the spawner so i it gets detsroyed when u exit the minigame- nizak
        currentItem = Instantiate(selectedItem, canvas.transform.position + selectedItem.transform.position, canvas.transform.rotation, this.transform );
        SpriteRenderer spriteRenderer = currentItem.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = canvasLayer+1;


        currentItemIsFishingGear = isBuy; 

    }
    //AI assisted - checks if the current item is spawned, and then destroys it
    public void ClearItem()
    {
        if(currentItem != null)
           {
            Destroy(currentItem);
            currentItem = null;
        }
    }
    //Awake runs when the object is intialized
    //The Canvas's sorting order is saved here 
    void Awake()
    {
        canvasLayer = canvas.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
