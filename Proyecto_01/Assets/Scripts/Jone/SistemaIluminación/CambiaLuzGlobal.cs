using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CambiaLuzGlobal : MonoBehaviour
{
    [SerializeField] GameObject luzGlobalGO;
    Light2D luzGlobal;
    [SerializeField] float intensidadNueva = 0.9f;

    void Start()
    {
       luzGlobal = luzGlobalGO.GetComponent<Light2D>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision) // Cuando Alex entra
    {
        if (collision.CompareTag("Player"))
        {
            luzGlobal.intensity = intensidadNueva;
            GameManager.instance.GuardarDatosEscena();
        }
    }
}
