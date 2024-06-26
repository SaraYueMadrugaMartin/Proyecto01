using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LucesTodas : MonoBehaviour
{
    [SerializeField] GameObject luces;
    Light2D luzGlobal;
    
    private static bool apagaLuces = false;
    private float intensidadGlobal;
    private float duracionTransicion = 5.0f; // Duración de la transición en segundos

    void Start()
    {
        luzGlobal = GetComponent<Light2D>();
        intensidadGlobal = luzGlobal.intensity;
    }


    public void CambiaEstadoLuces()
    {
        apagaLuces = !apagaLuces;

        if (apagaLuces) // Se apagan todas las luces
        {
            Debug.Log("apaga");
            luces.SetActive(false);
            StartCoroutine(CambiarIntensidadLuz(0f, duracionTransicion));
        }
        else // Se encienden todas las luces
        {
            Debug.Log("enciende");
            luces.SetActive(true);
            StartCoroutine(CambiarIntensidadLuz(intensidadGlobal, duracionTransicion));
        }
    }

    // Para el cambio progresivo de la luz
    private IEnumerator CambiarIntensidadLuz(float intensidadObjetivo, float duracion)
    {
        float intensidadInicial = luzGlobal.intensity;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            luzGlobal.intensity = Mathf.Lerp(intensidadInicial, intensidadObjetivo, tiempoTranscurrido / duracion);
            yield return null;
        }

        luzGlobal.intensity = intensidadObjetivo; // Asegurarse de que la intensidad final sea exacta
    }
}
