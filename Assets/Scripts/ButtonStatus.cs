using UnityEngine;

public class ButtonStatus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("nothing");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        Debug.Log("clicked");
    }

    void OnMouseUp(){
        Debug.Log("not clicked");
    }
}
