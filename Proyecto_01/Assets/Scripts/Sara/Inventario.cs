using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    //[SerializeField] private GameObject panelInformacion;
    [SerializeField] private GameObject playerStats;
    [SerializeField] private List<ObjetoPanelInfo> objetosPanelesInformacion;

    [SerializeField] private GameObject botonAtrasInfo;

    public HuecosInventario[] huecosInventario;

    public static Inventario Instance;
    private bool estadoInvent = false;
    private int llaveID = 0;

    public bool TieneObjeto(string nombreItem)
    {
        foreach (HuecosInventario hueco in huecosInventario)
        {
            if (hueco.nombreItem == nombreItem)
            {
                return true;
            }
        }
        return false;
    }

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown("i") && estadoInvent)
        {
            Time.timeScale = 1;
            inventario.SetActive(false);
            estadoInvent = false;
        }
        else if (Input.GetKeyDown("i") && !estadoInvent)
        {
            Time.timeScale = 0;
            inventario.SetActive(true);
            estadoInvent = true;
        }
    }

    public void AñadirObjeto(string nombreItem, Sprite sprite)
    {
        Debug.Log("Añadiendo objeto al inventario: " + nombreItem);
        for (int i = 0; i < huecosInventario.Length; i++)
        {
            if (huecosInventario[i].estaCompleto == false)
            {
                huecosInventario[i].AñadirObjeto(nombreItem, sprite);
                return;
            }
        }
    }

    public void VaciarHueco(string nombreItem)
    {
        foreach (HuecosInventario hueco in huecosInventario)
        {
            if (hueco.nombreItem == nombreItem)
            {
                hueco.VaciarHueco();
                break;
            }
        }
    }

    public void DeseleccionarObjetos()
    {
        for(int i = 0; i < huecosInventario.Length; i++)
        {
            huecosInventario[i].panelSeleccion.SetActive(false);
            huecosInventario[i].panelBotones.SetActive(false);
            huecosInventario[i].objetoSeleccionado = false;
        }
    }

    public void InfoObjetos()
    {
        for (int i = 0; i < huecosInventario.Length; i++)
        {
            if (huecosInventario[i].objetoSeleccionado)
            {
                string nombreObjeto = huecosInventario[i].nombreItem;

                ObjetoPanelInfo objetoPanelInfo = objetosPanelesInformacion.Find(obj => obj.nombreObjeto == nombreObjeto);
                if (objetoPanelInfo != null)
                {
                    DesactivarPanelesInformacion();
                    playerStats.SetActive(false);
                    objetoPanelInfo.panelInfo.SetActive(true);
                    botonAtrasInfo.SetActive(true);
                }
                break;
            }
        }
    }

    private void DesactivarPanelesInformacion()
    {
        foreach (ObjetoPanelInfo objetoPanelInfo in objetosPanelesInformacion)
        {
            if (objetoPanelInfo.panelInfo != null)
            {
                objetoPanelInfo.panelInfo.SetActive(false);
                botonAtrasInfo.SetActive(false);
            }
        }
    }

    public void QuitarPanelesInformacion()
    {
        foreach (ObjetoPanelInfo objetoPanelInfo in objetosPanelesInformacion)
        {
            if (objetoPanelInfo.panelInfo != null)
            {
                objetoPanelInfo.panelInfo.SetActive(false);
                playerStats.SetActive(true);
                botonAtrasInfo.SetActive(false);
            }
        }
    }


    public void RecibeIDLlave(int ID)
    {
        llaveID = ID;
    }
    public int BuscaIDLlave()
    {
        return llaveID;
    }
}

[System.Serializable]
public class ObjetoPanelInfo
{
    public string nombreObjeto;
    public GameObject panelInfo;
}
