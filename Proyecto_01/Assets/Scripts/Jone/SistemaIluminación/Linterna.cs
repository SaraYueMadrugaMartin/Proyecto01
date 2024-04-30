using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Linterna : MonoBehaviour
{
    private bool enciendeLinterna = false;
    private bool tieneLinterna = true;
    Light2D luz;

    void Start()
    {
        luz = GetComponent<Light2D>();
    }

    void Update()
    {
        // Falta incluír la comprobación de que tenga la linterna
        if (tieneLinterna && Input.GetKeyDown("l"))
        {
            enciendeLinterna = !enciendeLinterna;
        }

        if (enciendeLinterna)
        {
            luz.enabled = true;
        }
        else
            luz.enabled = false;
    }
}
