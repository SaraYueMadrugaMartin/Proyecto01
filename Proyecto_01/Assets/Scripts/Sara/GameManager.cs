using UnityEngine.SceneManagement;
using UnityEngine;

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
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex != 0)
                if (!Cinematicas.CineReproduciendo && !Inventario.estadoInvent)
                {
                    // Manejo normal del panel de pausa
                    if (Time.timeScale != 0)
                        Pausa.TriggerPause();
                    else
                        Pausa.TriggerResume();
                }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            saveManager.CargarEstadoEscena();
        }
    }

    public void GuardarDatosEscena()
    {
        saveManager.GuardarEstadoEscena();
    }

    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("carga datos");
        SceneManager.sceneLoaded += CargarCambiosEscenaGuardados;
    }

    public void CargarCambiosEscenaGuardados(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= CargarCambiosEscenaGuardados;
        saveManager.CargarEstadoEscena();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        UIManager.Instance.CargarPantallaPausa();
    }
    void ResumeGame()
    {
        Time.timeScale = 1f;
        UIManager.Instance.QuitarPantallaPausa();
    }

    public void VolverAlInicio()
    {
        SceneManager.LoadScene(0);
    }
}
