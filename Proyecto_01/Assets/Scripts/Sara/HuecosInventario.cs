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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
