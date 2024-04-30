using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuecosInventario : MonoBehaviour
{
    [SerializeField] private Image fotoItem;

    public string nombreItem;
    public int cantidad;
    public Sprite sprite;
    public bool estaCompleto;
    public Vector2 coordItem;

    public void AñadirObjeto(string nombreItem, Sprite sprite)
    {
        this.nombreItem = nombreItem;
        this.sprite = sprite;
        estaCompleto = true;
        fotoItem.sprite = sprite;
    }

    public void VaciarHueco()
    {
        nombreItem = "";
        cantidad = 0;
        sprite = null;
        estaCompleto = false;
        fotoItem.sprite = null;
    }
}
