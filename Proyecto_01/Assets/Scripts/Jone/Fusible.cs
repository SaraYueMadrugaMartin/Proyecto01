using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fusible : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D circleCollider;

    [SerializeField] GameObject panelAviso;
    [SerializeField] TextMeshProUGUI textoAviso;

    private bool tieneLinterna = false;
    [SerializeField] GameObject inventarioCanvas;
    Inventario inventario;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        inventario = inventarioCanvas.GetComponent<Inventario>();
    }

    private void Update()
    {
        tieneLinterna = inventario.TieneObjeto("Linterna");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tieneLinterna)
        {
            capsuleCollider.enabled = false;
            circleCollider.enabled = false;
        }
        else
        {
            textoAviso.text = "No debería quitar el fusible\r\nsin tener una linterna.";
            panelAviso.SetActive(true);
            StartCoroutine(EsperaSegundos());
        }
    }
    IEnumerator EsperaSegundos()
    {
        yield return new WaitForSecondsRealtime(2f);
        panelAviso.SetActive(false);
    }

}
