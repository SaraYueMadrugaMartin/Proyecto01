using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelesInteracciones : MonoBehaviour
{
    [SerializeField] private GameObject panelInteraccion;
    [SerializeField] private TextMeshProUGUI infoPanelInteraccion;
    [SerializeField] private float duracionPanel = 2f;

    void Start()
    {
        panelInteraccion.SetActive(false);
        //infoPanelInteraccion = new TextMeshProUGUI();
    }

    void Update()
    {
        
    }

    public void AparecerPanelInteraccion(string nombreItem)
    {
        panelInteraccion.SetActive(true);

        switch (nombreItem)
        {
            case "Documento":
                infoPanelInteraccion.text = "Se ha añadido al inventario un documento.";
                break;
            case "Llave":
                infoPanelInteraccion.text = "Se ha añadido al inventario una llave.";
                break;
            case "Tinta":
                infoPanelInteraccion.text = "Se ha añadido al inventario un bote de tinta.";
                break;
            case "Bate":
                infoPanelInteraccion.text = "Se ha añadido al inventario un bate.";
                break;
            case "Botiquin":
                infoPanelInteraccion.text = "Se ha añadido al inventario un botiquin.";
                break;
            case "Moneda":
                infoPanelInteraccion.text = "Se ha añadido al inventario una moneda.";
                break;
            case "Diario":
                infoPanelInteraccion.text = "Se ha añadido al inventario un diario.";
                break;
            case "Municion":
                infoPanelInteraccion.text = "Se ha añadido al inventario una caja de munición.";
                break;
        }

        StartCoroutine(DesactivarPanelInteraccion());
    }

    IEnumerator DesactivarPanelInteraccion()
    {
        yield return new WaitForSeconds(duracionPanel);
        panelInteraccion.SetActive(false);
    }
}
