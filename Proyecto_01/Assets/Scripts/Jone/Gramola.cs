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
                // Salta texto de interacci�n "Tienes X monedas. �Desea usar 1 moneda para escuchar una canci�n?" con botones s� o no
                // En el caso de que le de a que s�
                Player.monedasCorr -= 1;
                // Reproduce m�sica y espera a que termine el audio
                FindObjectOfType<AudioManager>().Play("Gramola");
                inventario.VaciarHueco("Moneda");
                Player.corrupcion = 0;
                textoAviso.text = "Reproduciendo canci�n.";
                StartCoroutine(EsperaSegundos());
            }
            else
            {
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
