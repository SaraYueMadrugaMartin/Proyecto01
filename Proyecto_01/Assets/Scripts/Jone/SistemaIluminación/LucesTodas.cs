using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LucesTodas : MonoBehaviour
{
    [SerializeField] GameObject luces;
    Light2D luzGlobal;
    
    private bool apagaLuces = false;
    private float intensidadGlobal;

    void Start()
    {
        luzGlobal = GetComponent<Light2D>();
        intensidadGlobal = luzGlobal.intensity;
    }

    void Update()
    {
        if (Input.GetKeyDown("q")) // Esto hay que cambiarlo a un evento
        {
            apagaLuces = !apagaLuces;
        }

        if (apagaLuces) // Se apagan todas las luces
        {           
            luces.SetActive(false);
            luzGlobal.intensity = 0f;
        }
        else // Se encienden todas las luces
        {
            luces.SetActive(true);
            luzGlobal.intensity = intensidadGlobal;
        }           
    }
}
