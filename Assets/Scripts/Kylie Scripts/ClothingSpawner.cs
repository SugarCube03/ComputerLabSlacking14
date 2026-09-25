using System;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class ClothingSpawner : MonoBehaviour
{

    [Serializable] 
    public class Clothing
    {
        public GameObject prefab;
        public bool isFishingGear;
    }

    [SerializeField] private Clothing[] clothingItems;
    [SerializeField] private Transform spawnPoint;

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
        {
            
            randomIndex = UnityEngine.Random.Range(0, clothingItems.Length);
        }

        Clothing selectedClothing = clothingItems[randomIndex];

        //i made ot so that the prefabs u make are children of the spawner so i it gets detsroyed when u exit the minigame- nizak
        currentItem = Instantiate(selectedClothing.prefab, spawnPoint.position, spawnPoint.rotation, this.transform );

        currentItemIsFishingGear = selectedClothing.isFishingGear; 

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
