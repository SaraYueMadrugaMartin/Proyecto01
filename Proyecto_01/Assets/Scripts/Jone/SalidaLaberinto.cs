using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalidaLaberinto : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float distanciaObjetivo = 2f;
    private float distanciaInicial;
    private float duracionCambio = 2f;
    public static BoxCollider2D triggerSalir;

    private void Start()
    {
        distanciaInicial = virtualCamera.m_Lens.OrthographicSize;
        triggerSalir = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // Cuando Alex entra
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CambiaCamara());
            triggerSalir.enabled = false;
            EntradaLaberinto.triggerEntrar.enabled = true;
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
