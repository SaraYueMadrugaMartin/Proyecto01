using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[System.Serializable]
public class ObjetoPanelInfo
{
    public string nombreObjeto;
    public GameObject panelInfo;
}

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    [SerializeField] private List<ObjetoPanelInfo> objetosPanelesInformacion;
    [SerializeField] private GameObject botonAtrasInfo;
    [SerializeField] private GameObject fondoPanelInfo;
    [SerializeField] private GameObject panelVidaXela;
    [SerializeField] private GameObject panelAviso;
    [SerializeField] private TextMeshProUGUI textoAviso;

    private Items[] items;

    private List<Items> objetosRegistrados = new List<Items>();

    public HuecosInventario[] huecosInventario;

    public bool objetoEnInventario = false;

    public static Inventario Instance;
    public static bool estadoInvent = false;
    private int llaveID = 0;

    SFXManager sfxManager;

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
        inventario.SetActive(false); // Acordarse de tenerlo activo en la escena, sino no lee bien.
        //objetoEstaEnInventario = new List<bool>();
        fondoPanelInfo.SetActive(false);
        sfxManager = FindObjectOfType<SFXManager>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && estadoInvent)
        {            
            Time.timeScale = 1;
            sfxManager.PlaySFX(sfxManager.clipsDeAudio[9]);
            StopCorazonSound();
            inventario.SetActive(false);
            DesactivarPanelesInformacion();
            estadoInvent = false;
            DeseleccionarObjetos();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (EntradaFinal.salaFinal)
                panelVidaXela.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !estadoInvent)
        {
            sfxManager.PlaySFX(sfxManager.clipsDeAudio[8]);
            PlayCorazonSound(sfxManager.clipsDeAudio[14]);
            Time.timeScale = 0;
            inventario.SetActive(true);
            estadoInvent = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            panelVidaXela.SetActive(false);
        }
    }

    public void AñadirObjeto(string nombreItem, Sprite sprite, int idLlave = -1)
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
                        hueco.AñadirObjeto(nombreItem, sprite, idLlave); // Pasar el ID de la llave
                        objetoEnInventario = true;
                        DeseleccionarObjetos();
                        Items item = FindObjectsOfType<Items>().FirstOrDefault(obj => obj.nombreItem == nombreItem);
                        if (item != null)
                        {
                            RegistrarObjeto(item);
                        }
                        return;
                    }
                }
            }
        }
    }

    public void VaciarHueco(string nombreItem)
    {
        VaciarHueco(nombreItem, -1); // Llamar a la versión con ID pasando un valor por defecto
    }

    public void VaciarHueco(string nombreItem, int idLlave)
    {
        if (nombreItem == "Llave" && idLlave != -1)
        {
            foreach (HuecosInventario hueco in huecosInventario)
            {
                if (hueco.nombreItem == nombreItem && hueco.idLlave == idLlave)
                {
                    hueco.VaciarHueco();
                    DeseleccionarObjetos();
                    return;
                }
            }
        }
        else
        {
            foreach (HuecosInventario hueco in huecosInventario)
            {
                if (hueco.nombreItem == nombreItem)
                {
                    hueco.VaciarHueco();
                    DeseleccionarObjetos();
                    break;
                }
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
                    fondoPanelInfo.SetActive(true);
                    objetoPanelInfo.panelInfo.SetActive(true);
                    botonAtrasInfo.SetActive(true);
                    DeseleccionarObjetos();
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
                fondoPanelInfo.SetActive(false);
                objetoPanelInfo.panelInfo.SetActive(false);
                botonAtrasInfo.SetActive(false);
            }
        }
    }

    public void BotonQuitarPanelesInformacion()
    {
        foreach (ObjetoPanelInfo objetoPanelInfo in objetosPanelesInformacion)
        {
            if (objetoPanelInfo.panelInfo != null)
            {
                fondoPanelInfo.SetActive(false);
                objetoPanelInfo.panelInfo.SetActive(false);
                botonAtrasInfo.SetActive(false);
            }
        }
    }

    public void RecibeIDLlave(int ID)
    {
        llaveID = ID;
    }
    public List<int> BuscaIDsLlaves()
    {
        List<int> idsLlaves = new List<int>();
        foreach (HuecosInventario hueco in huecosInventario)
        {
            if (hueco.nombreItem == "Llave" && hueco.idLlave != -1)
            {
                idsLlaves.Add(hueco.idLlave);
            }
        }
        return idsLlaves;
    }

    public void EliminarLlavePorID(int idLlave)
    {
        foreach (HuecosInventario hueco in huecosInventario)
        {
            if (hueco.nombreItem == "Llave" && hueco.idLlave == idLlave)
            {
                hueco.VaciarHueco();
                break;
            }
        }
    }

    public void DejarObjeto()
    {
        for (int i = 0; i < huecosInventario.Length; i++)
        {
            if (huecosInventario[i].objetoSeleccionado)
            {
                string nombreObjeto = huecosInventario[i].nombreItem;

                Debug.Log("Objeto seleccionado: " + nombreObjeto);

                if (!string.IsNullOrEmpty(nombreObjeto))
                {
                    if (nombreObjeto == "Llave" || nombreObjeto == "Fusible")
                    {
                        textoAviso.text = "Este objeto no se puede dejar";
                        StartCoroutine(EsperaSegundosPanelAviso());
                        return;
                    }

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
                        DeseleccionarObjetos();

                        break;
                    }
                }
            }
        }
    }

    IEnumerator EsperaSegundosPanelAviso()
    {
        panelAviso.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        panelAviso.SetActive(false);
    }

    public void RegistrarObjeto(Items item)
    {
        if (!objetosRegistrados.Contains(item))
        {
            objetosRegistrados.Add(item);
        }
    }

    public bool GetObjetoEnInventario()
    {
        return objetoEnInventario;
    }

    public List<Items> GetObjetosEnInventario()
    {
        return objetosRegistrados;
    }

    public void QuitarInventario()
    {
        Time.timeScale = 1;
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[9]);
        StopCorazonSound();
        inventario.SetActive(false);
        estadoInvent = false;
    }

    public void PlayCorazonSound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxManager.SFXScore.clip = clip;
            sfxManager.SFXScore.loop = true;
            sfxManager.SFXScore.Play();
        }
    }

    public void StopCorazonSound()
    {
        sfxManager.SFXScore.Stop();
    }
}
