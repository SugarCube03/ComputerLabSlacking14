using UnityEngine;

public class EndSceneSounds : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}
