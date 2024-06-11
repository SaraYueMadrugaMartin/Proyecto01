using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SalidaLaberinto : MonoBehaviour
{
    // Para cambiar el tamaño del objetivo de la cámara
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float distanciaObjetivo = 2f;
    private float distanciaInicial;
    private float duracionCambio = 2f;

    // Para cambiar el tamaño de la linterna
    [SerializeField] GameObject linterna;
    Light2D luzLinterna;
    private float radioInicial;

    // Para cambiar la intensidad de la luz
    [SerializeField] GameObject luzGlobalGO;
    Light2D luzGlobal;
    private float intensidadInicial;

    public static BoxCollider2D triggerSalir;
    
    private void Start()
    {
        distanciaInicial = virtualCamera.m_Lens.OrthographicSize;
        triggerSalir = GetComponent<BoxCollider2D>();
        luzGlobal = luzGlobalGO.GetComponent<Light2D>();
        intensidadInicial = EntradaLaberinto.intensidadInicial;
        radioInicial = EntradaLaberinto.radioInicial;
    }

    private void OnTriggerEnter2D(Collider2D collision) // Cuando Alex entra
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CambiaCamara());
            triggerSalir.enabled = false;
            EntradaLaberinto.triggerEntrar.enabled = true;
            luzLinterna = linterna.GetComponent<Light2D>();
            luzLinterna.pointLightOuterRadius = radioInicial;
            luzGlobal.intensity = intensidadInicial;
        }
    }

    // Corrutina para hacer que el cambio de la cámara no sea brusco
    private IEnumerator CambiaCamara()
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
}
