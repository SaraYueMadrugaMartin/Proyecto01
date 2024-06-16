using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelesInteracciones : MonoBehaviour
{
    [SerializeField] private GameObject panelInteraccion;
    [SerializeField] private GameObject panelAvisoInventarioCompleto;

    [SerializeField] private TextMeshProUGUI infoPanelInteraccion;

    void Start()
    {
        panelInteraccion.SetActive(false);
        panelAvisoInventarioCompleto.SetActive(false);
    }

    public void AparecerPanelInteraccion(string nombreItem)
    {
        panelInteraccion.SetActive(true);

        switch (nombreItem)
        {
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
            case "VHS":
                infoPanelInteraccion.text = "Se ha añadido al inventario un VHS.";
                break;
            case "Municion":
                infoPanelInteraccion.text = "Se ha añadido al inventario una caja de munición.";
                break;
            case "ValvulaCabeza":
                infoPanelInteraccion.text = "Se ha añadido al inventario una cabeza de válvula.";
                break;
            case "ValvulaCuerpo":
                infoPanelInteraccion.text = "Se ha añadido al inventario el cuerpo de la válvula.";
                break;
            case "Pistola":
                infoPanelInteraccion.text = "Se ha añadido al inventario una pistola.";
                break;
            case "Linterna":
                infoPanelInteraccion.text = "Se ha añadido al inventario una linterna.";
                break;
            default:
                infoPanelInteraccion.text = "Se ha añadido al inventario un objeto.";
                break;
        }

        StartCoroutine(DesactivarPanelInteraccion());
    }

    public void AparecerPanelAvisoInventarioCompleto()
    {
        panelAvisoInventarioCompleto.SetActive(true);
        StartCoroutine(DesactivarPanelAvisoInventarioCompleto());
    }

    IEnumerator DesactivarPanelInteraccion()
    {
        yield return new WaitForSecondsRealtime(1f);
        panelInteraccion.SetActive(false);
    }

    IEnumerator DesactivarPanelAvisoInventarioCompleto()
    {
        yield return new WaitForSecondsRealtime(2f);
        panelAvisoInventarioCompleto.SetActive(false);
    }
}
