using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelPuertaUnica : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI texto;
    private string textoPanel = "";

    public void Out()
    {
        textoPanel = "Parece que esta puerta no lleva a ningún lado.";
        texto.text = textoPanel;
        gameObject.SetActive(true);
        StartCoroutine(DesactivarPanel());
    }

    public void In()
    {
        textoPanel = "Vaya... estoy atascada aquí.";
        texto.text = textoPanel;
        gameObject.SetActive(true);
        StartCoroutine(DesactivarPanel());
    }


    IEnumerator DesactivarPanel()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
    }
}
