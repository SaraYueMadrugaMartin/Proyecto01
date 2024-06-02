using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaFinal : MonoBehaviour
{
    [SerializeField] GameObject barrera;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float distanciaObjetivo = 3.5f;
    private float distanciaInicial;
    private float duracionCambio = 2f;
    private BoxCollider2D trigger;
    public static bool salaFinal = false;
    static bool playerMuere;

    [SerializeField] GameObject panelVidaXela;

    private void Start()
    {
        distanciaInicial = virtualCamera.m_Lens.OrthographicSize;
        trigger = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (playerMuere)
        {
            panelVidaXela.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Cuando Alex entra
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CambiaCamara());
            Invoke("ActivaBarrera", 2f);
            trigger.enabled = false;
            salaFinal = true;
            panelVidaXela.SetActive(true);
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

    private void ActivaBarrera()
    {
        barrera.SetActive(true);
    }

    public static void DesactivaPanel()
    {
        playerMuere = true;
    }
}
