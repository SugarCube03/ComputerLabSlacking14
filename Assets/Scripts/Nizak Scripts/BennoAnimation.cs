using UnityEngine;

public class BennoAnimation : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite Teaching;
    public Sprite Turn;
    public Sprite Check;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void BennoTeach()
    {
        spriteRenderer.sprite = Teaching;
    }

    public void BennoTurn()
    {
        spriteRenderer.sprite = Turn;
    }

    public void BennoCheck()
    {
        spriteRenderer.sprite = Check;
    }
}
