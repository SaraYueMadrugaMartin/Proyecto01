using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Vector2 coordenadasItem;
    private string nombreItem;
    private Sprite spriteItem;

    public Item(Vector2 coordenadasItem, string nombreItem, Sprite spriteItem)
    {
        this.coordenadasItem = coordenadasItem;
        this.nombreItem = nombreItem;
        this.spriteItem = spriteItem;
    }

    public Vector2 GetCoordItem()
    {
        return this.coordenadasItem;
    }

    public Sprite GetSpriteItem()
    {
        return this.spriteItem;
    }

    public string GetNombreItem()
    {
        return this.nombreItem;
    }

    public void SetCoordItem(Vector2 coordenadasItem)
    {
        this.coordenadasItem = coordenadasItem.normalized;
    }

    public void SetSpriteItem(Sprite spriteItem)
    {
        this.spriteItem = spriteItem;
    }

    public void SetNombreItem(string nombreItem)
    {
        this.nombreItem = nombreItem;
    }
}
