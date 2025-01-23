using UnityEngine;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    public int Value { get; private set; }       
    public Sprite Sprite { get; private set; }  
    public bool IsInteractable { get; private set; }
    

    private SpriteRenderer spriteRenderer;
   

void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on Card!");
        }
    }


    public void InitializeCard(int value, Sprite sprite)
    {
        Value = value;
        Sprite = sprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }


    public void SetInteractable(bool interactable)
    {
        IsInteractable = interactable;
        spriteRenderer.color = interactable ? Color.white : Color.gray; 
    }

    
    public void SetCard(int value, Sprite sprite)
    {
        InitializeCard(value, sprite);
    }

    
    public void FlipCard(bool faceUp)
    {
        spriteRenderer.enabled = faceUp;
    }
}
