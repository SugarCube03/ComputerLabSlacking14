using Unity.VisualScripting;
using UnityEngine;

public class BennoManager : MonoBehaviour
{
    private State currentState;

    [SerializeField] private float minDistractedTime;
    [SerializeField] private float maxDistractedTime;
    [SerializeField] private float turnBackWarning;
    public enum State
    {
        Teaching,
        Turning,
        Checking
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentState = State.Teaching;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
