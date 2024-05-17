using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    [SerializeField] private GameObject playerStats;
    [SerializeField] private List<ObjetoPanelInfo> objetosPanelesInformacion;
    [SerializeField] private GameObject botonAtrasInfo;

    public HuecosInventario[] huecosInventario;

    private Items devolverItems;

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
            if (!InventarioCompleto())
            {
                foreach (HuecosInventario hueco in huecosInventario)
                {
                    if (!hueco.estaCompleto)
                    {
                        hueco.AñadirObjeto(nombreItem, sprite);
                        return; // Parar el bucle cuando encuentra el hueco vacío y añade el nuevo objeto.
                    }
                }
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

    public bool InventarioCompleto()
    {
        int huecosLlenos = huecosInventario.Count(hueco => hueco.estaCompleto);
        return huecosLlenos >= 9;
    }

    public void DeseleccionarObjetos()
    {
        foreach (HuecosInventario hueco in huecosInventario)
        {
            hueco.panelSeleccion.SetActive(false);
            foreach (var panelBoton in hueco.panelesBotones.Values)
            {
                panelBoton.SetActive(false);
            }
            hueco.objetoSeleccionado = false;
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
