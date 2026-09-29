using System;
using UnityEngine;

public class ClothingSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] FishingGear;
    [SerializeField] private GameObject[] NonFishingGear;
    [SerializeField] private Canvas canvas;

    [Range(0f, 1f)]
    [SerializeField] private float buyChance = 0.5f;
    
    private int canvasLayer;


    private GameObject currentItem;
    private bool currentItemIsFishingGear;

    public bool CurrentItemIsFishingGear => currentItemIsFishingGear;

    int randomIndex;
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
 
    public void ClearItem()
    {
        if(currentItem != null)
        {
            Destroy(currentItem);
            currentItem = null;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        canvasLayer = canvas.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
