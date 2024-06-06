using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TextMeshProUGUI textoAviso;

    [SerializeField] GameObject panelPausa;


    private void OnEnable()
    {
        Pausa.OnPause += CargarPantallaPausa;
        Pausa.OnResume += QuitarPantallaPausa;
    }
    private void OnDisable()
    {
        Pausa.OnPause -= CargarPantallaPausa;
        Pausa.OnResume -= QuitarPantallaPausa;
    }

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

    public void CargarPantallaPausa()
    {
        panelPausa.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitarPantallaPausa()
    {
        panelPausa.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PulsarBotonContinuar()
    {
        Pausa.TriggerResume();
        //GameManager.instance.CargarEscena();
    }

    public void PulsarBotonSalir()
    {        
        Pausa.TriggerResume();
        ActualizarTextoAviso("Se perderán los datos que no se hayan guardado.\nEstá seguro de que desea salir?");       
    }

    public void PulsarBotonSi()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void ActualizarTextoAviso(string aviso)
    {
        textoAviso.text = aviso;
    }
}
