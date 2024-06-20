using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gramola : MonoBehaviour
{
    [SerializeField] private Inventario inventario;
    private bool estaTocando = false;
    [SerializeField] GameObject panelAviso;
    [SerializeField] TextMeshProUGUI textoAviso;

    private void Update()
    {
        if (estaTocando && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interactua");            
            if (Player.monedasCorr >= 1)
            {
                Debug.Log("Tengo moneda");
                FindObjectOfType<AudioManager>().Play("Gramola");
                inventario.VaciarHueco("Moneda");
                Player.corrupcion = 0;
                textoAviso.text = "Reproduciendo canción.";
                StartCoroutine(EsperaSegundos());
                Player.monedasCorr -= 1;
            }
            else
            {
                Debug.Log("No tengo moneda");
                textoAviso.text = "Necesitas una moneda para usar la gramola.";
                StartCoroutine(EsperaSegundos());
            }               
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
            estaTocando = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            estaTocando = false;
    }

    IEnumerator EsperaSegundos()
    {
        panelAviso.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        panelAviso.SetActive(false);
    }
}
