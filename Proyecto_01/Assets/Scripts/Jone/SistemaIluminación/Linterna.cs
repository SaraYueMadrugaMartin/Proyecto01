using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Linterna : MonoBehaviour
{
    private bool enciendeLinterna = false;
    private bool tieneLinterna = false;
    Light2D luz;
    [SerializeField] GameObject inventarioCanvas;
    Inventario inventario;

    void Start()
    {
        luz = GetComponent<Light2D>();
        inventario = inventarioCanvas.GetComponent<Inventario>();
    }

    void Update()
    {
        tieneLinterna = inventario.TieneObjeto("Linterna");
        if (tieneLinterna && Input.GetKeyDown("f"))
        {
            enciendeLinterna = !enciendeLinterna;
            CambiaEstado();
        }        
    }

    void CambiaEstado()
    {
        if (enciendeLinterna)
        {
            luz.enabled = true;
        }
        else
            luz.enabled = false;
    }
}
