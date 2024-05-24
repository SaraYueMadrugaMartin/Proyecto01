using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ObjetoPanelInfo
{
    public string nombreObjeto;
    public GameObject panelInfo;
}

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    [SerializeField] private GameObject playerStats;
    [SerializeField] private List<ObjetoPanelInfo> objetosPanelesInformacion;
    [SerializeField] private GameObject botonAtrasInfo;

    private Items[] items;

    private List<Items> objetosRegistrados = new List<Items>();

    public HuecosInventario[] huecosInventario;

    public bool objetoEnInventario = false;
    //private List<bool> objetoEstaEnInventario;

    //private Items devolverItems;

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
        items = FindObjectsOfType<Items>();
        //objetoEstaEnInventario = new List<bool>();
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
                //ObjetoEstaEnInventario(true);
                foreach (HuecosInventario hueco in huecosInventario)
                {
                    if (!hueco.estaCompleto)
                    {
                        hueco.AñadirObjeto(nombreItem, sprite);
                        objetoEnInventario = true;

                        // Buscar el objeto en la escena y registrarlo
                        Items item = FindObjectsOfType<Items>().FirstOrDefault(obj => obj.nombreItem == nombreItem);
                        if (item != null)
                        {
                            RegistrarObjeto(item);
                        }
                        else
                        {
                            Debug.LogWarning("No se encontró el objeto en la escena: " + nombreItem);
                        }

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

    public void DejarObjeto()
    {
        Debug.Log("Intentando dejar objeto...");

        for (int i = 0; i < huecosInventario.Length; i++)
        {
            if (huecosInventario[i].objetoSeleccionado)
            {
                string nombreObjeto = huecosInventario[i].nombreItem;

                Debug.Log("Objeto seleccionado: " + nombreObjeto);

                if (!string.IsNullOrEmpty(nombreObjeto))
                {
                    // Buscamos el objeto en la lista de objetos registrados
                    Items objeto = objetosRegistrados.FirstOrDefault(item => item.nombreItem == nombreObjeto);

                    if (objeto != null)
                    {
                        foreach (Items item in items)
                        {
                            item.objetoRecogido = false;
                            item.ObjetoRecogido(false);
                        }

                        Vector3 posicionJugador = GameObject.FindWithTag("Player").transform.position;
                        objeto.MoverYActivar(posicionJugador);

                        foreach (var panelBoton in huecosInventario[i].panelesBotones.Values)
                        {
                            panelBoton.SetActive(false);
                        }
                        huecosInventario[i].panelSeleccion.SetActive(false);
                        huecosInventario[i].VaciarHueco();

                        Debug.Log("Objeto dejado: " + nombreObjeto);
                        break;
                    }
                }
            }
        }
    }

    public void RegistrarObjeto(Items item)
    {
        if (!objetosRegistrados.Contains(item))
        {
            objetosRegistrados.Add(item);
        }
    }

    public void DesregistrarObjeto(Items item)
    {
        if (objetosRegistrados.Contains(item))
        {
            objetosRegistrados.Remove(item);
        }
    }

    /*private void ObjetoEstaEnInventario(bool value)
    {
        objetoEstaEnInventario.Add(value);
    }*/

    public bool GetObjetoEnInventario()
    {
        return objetoEnInventario;
    }
}
