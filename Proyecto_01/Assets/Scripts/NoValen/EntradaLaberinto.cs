using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EntradaLaberinto : MonoBehaviour
{
    // Para cambiar el tamaño del objetivo de la cámara
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float distanciaLaberinto = 3.5f;
    private float distanciaInicial;
    private float duracionCambio = 2f;

    // Para cambiar el tamaño de la linterna
    [SerializeField] GameObject linterna;
    Light2D luzLinterna;
    public static float radioInicial = 1.8f;
    [SerializeField] float radioLaberinto = 3.5f;

    // Para cambiar la intensidad de la luz
    [SerializeField] GameObject luzGlobalGO;
    Light2D luzGlobal;
    public static float intensidadInicial;
    [SerializeField] float intensidadLaberinto;

    public static BoxCollider2D triggerEntrar;

    private void Start()
    {
        distanciaInicial = virtualCamera.m_Lens.OrthographicSize;
        triggerEntrar = GetComponent<BoxCollider2D>();

        luzGlobal = luzGlobalGO.GetComponent<Light2D>();
        intensidadInicial = luzGlobal.intensity;       
    }

    private void OnTriggerEnter2D(Collider2D collision) // Cuando Alex entra
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CambiaCamara());
            triggerEntrar.enabled = false;
            SalidaLaberinto.triggerSalir.enabled = true;
            luzLinterna = linterna.GetComponent<Light2D>();
            luzLinterna.pointLightOuterRadius = radioLaberinto;
            luzGlobal.intensity = intensidadLaberinto;
        }
    }

    // Corrutina para hacer que el cambio de la cámara no sea brusco
    private IEnumerator CambiaCamara()
    {
        float objetivo = distanciaLaberinto;
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
}
