using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CambiosLaberinto : MonoBehaviour
{
    private bool unaVez = true;
    private bool entra = true; // Para gestionar si entra o sale y cambiar parámetros según el caso

    // Para cambiar el tamaño del objetivo de la cámara
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float distanciaLaberinto = 3.5f;
    private float distanciaInicial;
    private float duracionCambio = 2f;

    // Para cambiar el tamaño de la linterna
    [SerializeField] GameObject linterna;
    [SerializeField] float radioLaberinto = 3.5f;
    public static float radioInicial = 1.8f;
    Light2D luzLinterna;

    // Para cambiar la intensidad de la luz
    [SerializeField] GameObject luzGlobalGO;
    [SerializeField] float intensidadLaberinto = 0.5f;
    public static float intensidadInicial;
    Light2D luzGlobal;

    private void Start()
    {
        distanciaInicial = virtualCamera.m_Lens.OrthographicSize;

        luzLinterna = linterna.GetComponent<Light2D>();

        luzGlobal = luzGlobalGO.GetComponent<Light2D>();
        intensidadInicial = luzGlobal.intensity;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (unaVez) // Para que solo haga el cambio una vez
            {
                if (entra)
                {
                    StartCoroutine(CambiaCam(distanciaLaberinto, distanciaInicial));
                    CambiaLinterna(radioLaberinto);
                    CambiaGlobalLight(intensidadLaberinto);
                    unaVez = false;
                    entra = false;
                }
                else
                {
                    StartCoroutine(CambiaCam(distanciaInicial, distanciaLaberinto));
                    CambiaLinterna(radioInicial);
                    CambiaGlobalLight(intensidadInicial);
                    unaVez = false;
                    entra = true;
                }                
            }
            StartCoroutine(EsperaUnPoco());
        }
    }

    // Corrutina para hacer que el cambio de la cámara no sea brusco
    private IEnumerator CambiaCam(float distanciaObjetivo, float distanciaInicial)
    {
        float objetivo = distanciaObjetivo;
        float inicio = distanciaInicial;
        float tiempo = 0f;

        while (tiempo < duracionCambio)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionCambio;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(inicio, objetivo, t);
            yield return null;
        }

        // Asegurarse de que el valor final sea exactamente el objetivo
        virtualCamera.m_Lens.OrthographicSize = objetivo;
    }
    private void CambiaLinterna(float radioObjetivo)
    {
        luzLinterna.pointLightOuterRadius = radioObjetivo;
    }

    private void CambiaGlobalLight(float intensidadObjetivo)
    {
        luzGlobal.intensity = intensidadObjetivo;
    }

    // Vuelve a poner la bandera en true después de esperar
    private IEnumerator EsperaUnPoco()
    {
        yield return new WaitForSecondsRealtime(1f);
        unaVez = true;
    }
}
