using UnityEngine;

public class ShipGraphics : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shipSpriteRenderer;



    public void InitGraphics(Sprite shipSprite)
    {
        shipSpriteRenderer.sprite = shipSprite;
        Debug.LogWarning("Ship Sprite loaded");
    }

}