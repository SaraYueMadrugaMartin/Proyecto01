using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SaveManager saveManager;

    [SerializeField] GameObject panelPausa;

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
            if (TelevisorVHS.CineReproduciendo)
            {
                // Delegar la pausa/reanudación a TelevisorVHS
                if (!TelevisorVHS.CineReproduciendo)
                {
                    Pausa.TriggerPause();
                }
                else
                {
                    Pausa.TriggerResume();
                }
            }
            else
            {
                // Manejo normal del panel de pausa
                if (Time.timeScale != 0)
                    Pausa.TriggerPause();
                else
                    Pausa.TriggerResume();
            }
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
