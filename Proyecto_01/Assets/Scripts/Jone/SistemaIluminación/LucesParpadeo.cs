using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LucesParpadeo : MonoBehaviour
{
    private Light2D luz;
    [SerializeField] private float esperaMin = 5f;
    [SerializeField] private float esperaMax = 20f;

    void Start()
    {
        luz = GetComponent<Light2D>();
        StartCoroutine(Parpadear());
    }

    IEnumerator Parpadear()
    {
        for (int i = 0; i < 4; i++)
        {
            luz.intensity = (i % 2 == 0) ? 0 : 1; // Alterna entre apagar (0) y encender (1) la luz
            yield return new WaitForSeconds(0.2f);
        }

        float tiempoEspera = Random.Range(esperaMin, esperaMax);
        yield return new WaitForSeconds(tiempoEspera); 
        StartCoroutine(Parpadear()); // Reinicia la corrutina
    }
}
