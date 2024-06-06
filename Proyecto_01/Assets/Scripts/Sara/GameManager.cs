using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SaveManager saveManager;

    private void OnEnable()
    {
        Pausa.OnPause += PauseGame;
        Pausa.OnResume += ResumeGame;
    }
    private void OnDisable()
    {
        Pausa.OnPause -= PauseGame;
        Pausa.OnResume -= ResumeGame;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        saveManager = SaveManager.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
                Pausa.TriggerPause();
            else
                Pausa.TriggerResume();
        }
    }

    public void GuardarDatosEscena()
    {
        saveManager.GuardarEstadoEscena();
    }

    public void ReiniciarEscena()
    {
        Debug.Log("carga datos");
        SceneManager.sceneLoaded += CargarCambiosEscenaGuardados;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CargarCambiosEscenaGuardados(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= CargarCambiosEscenaGuardados;
        saveManager.CargarEstadoEscena();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }
    void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
