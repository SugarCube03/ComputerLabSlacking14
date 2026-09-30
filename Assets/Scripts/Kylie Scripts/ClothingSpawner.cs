using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    
    private System.Random systemRandom = new System.Random();

    //Incorporation of rng for both fishing gear and non fishing gear 

    [SerializeField] private float buyChance = 0.5f;
    

    private int canvasLayer;

    //This GameObject stores the asset of the current item that is spawned
    private GameObject currentItem;

    //Type bool that will check whether the currently spawned item is fishing gear
    private bool currentItemIsFishingGear;
    public bool CurrentItemIsFishingGear => currentItemIsFishingGear;

    // dictionary
    private Dictionary<GameObject, bool> spawnDictionary= new Dictionary<GameObject,bool>();

    int randomIndex;

    //AI assisted - SpawnRandomItem allows other scripts to check
    //if the current item is fishing gear or non-fishing gear

    public void SpawnRandomItem()
    {
        if(currentItem != null)
        {
            Destroy(currentItem);
        } 


            GameObject selectedItem = spawnDictionary.ElementAt(systemRandom.Next(0, spawnDictionary.Count)).Key;
          

        //i made ot so that the prefabs u make are children of the spawner so i it gets detsroyed when u exit the minigame- nizak
        currentItem = Instantiate(selectedItem, canvas.transform.position + selectedItem.transform.position, canvas.transform.rotation, this.transform );
        SpriteRenderer spriteRenderer = currentItem.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = canvasLayer+1;


        currentItemIsFishingGear = spawnDictionary[selectedItem]; 
        spawnDictionary.Remove(selectedItem);
        

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

    // iterate through fishing list + add fishing prefab + true 
    // if already used, change to false
    private void ToDictionary()
    {
        for(int i = 0; i < FishingGear.Length; i++)
        {
            spawnDictionary.Add(FishingGear[i], true);
        }

        for(int i = 0; i < NonFishingGear.Length; i++)
        {
            spawnDictionary.Add(NonFishingGear[i], false);
        }


    }


    //Awake runs when the object is intialized
    //The Canvas's sorting order is saved here 
    void Awake()
    {
        canvasLayer = canvas.sortingOrder;
        ToDictionary();
    }

    public int GetSpawnDictionaryLength()
    {
        return spawnDictionary.Count();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
