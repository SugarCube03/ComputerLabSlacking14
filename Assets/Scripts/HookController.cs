using UnityEngine;

public class HookController : MonoBehaviour
{
    public FishingController fishingController;  //  drag FishRod object
    public Transform fishHoldPoint;               //  can use the transform by hook itself

    void OnTriggerEnter2D(Collider2D other)
    {
        Fish fish = other.GetComponent<Fish>();
        if (fish != null)
        {
            // set fish as the child object of hook, so it can move with hook
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;

            // inform FishingController switch to retracting state
            fishingController.CatchFish(fish);
        }
    }
}