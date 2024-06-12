using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject panelPausa;
    [SerializeField] GameObject panelAvisoSalir;
    [SerializeField] TextMeshProUGUI textoAviso;

    // Gestión de eventos
    private void OnEnable()
    {
        Pausa.OnPause += CargarPantallaPausa;
        Pausa.OnResume += QuitarPantallaPausa;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        Pausa.OnPause -= CargarPantallaPausa;
        Pausa.OnResume -= QuitarPantallaPausa;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        BuscarReferenciasUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BuscarReferenciasUI();
    }

    private void BuscarReferenciasUI()
    {
        GameObject canvas = GameObject.Find("CanvasPersistente");
        if (canvas != null)
        {
            panelPausa = canvas.transform.Find("PanelPausa").gameObject;
            panelAvisoSalir = canvas.transform.Find("PanelAviso").gameObject;
            textoAviso = panelAvisoSalir.transform.Find("TextoAviso").GetComponent<TextMeshProUGUI>();

            if (panelPausa == null || panelAvisoSalir == null || textoAviso == null)
            {
                Debug.LogWarning("No se encontraron todos los elementos necesarios.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto Canvas.");
        }
    }

    public void CargarPantallaPausa()
    {
        panelPausa.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitarPantallaPausa()
    {
        panelPausa.SetActive(false);
        panelAvisoSalir.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PulsarBotonContinuar()
    {
        Pausa.TriggerResume();
    }

    public void PulsarBotonSalir()
    {
        panelAvisoSalir.SetActive(true);

        ActualizarTextoAviso("Se perderán los datos que no se hayan guardado.\nEstá seguro de que desea salir?");
    }

    public void PulsarBotonSi()
    {
        Pausa.TriggerResume();
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        panelAvisoSalir.SetActive(false);
    }

    public void PulsarBotonNo()
    {
        panelAvisoSalir.SetActive(false);
    }

    public void ActualizarTextoAviso(string aviso)
    {
        textoAviso.text = aviso;
    }
}

