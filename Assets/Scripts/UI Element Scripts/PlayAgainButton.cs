using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void clicked()
    {
        SceneManager.LoadScene("MainGame");
    }
    
}
