using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TelevisorVHS : MonoBehaviour
{
    [SerializeField] private GameObject panelNoVHS;
    [SerializeField] private GameObject cintaVHS;
    [SerializeField] private VideoPlayer cinematica;
    [SerializeField] private GameObject panelPausa;
    private bool pausado = false;
    private bool reproduciendo = false;
    AudioManager audioManager;
    private float duracion;

    private Inventario inventario;
    private bool jugadorTocando = false;

    [SerializeField] GameObject puertaGO;
    MirrorPuerta puerta;

    public static bool CineReproduciendo { get; private set; } = false;

    private void Start()
    {
        audioManager = AudioManager.instance;
        inventario = FindObjectOfType<Inventario>();
        panelNoVHS.SetActive(false);
        puerta = puertaGO.GetComponent<MirrorPuerta>();
        cinematica = cintaVHS.GetComponent<VideoPlayer>();
        duracion = (float)cinematica.clip.length;
        cintaVHS.SetActive(false);
        panelPausa.SetActive(false);
    }

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            if (inventario != null)
            {
                if (inventario.TieneObjeto("VHS"))
                {
                    ReproducirVHS();
                    inventario.VaciarHueco("VHS");
                }
                else
                {
                    panelNoVHS.SetActive(true);
                    Debug.Log("No tienes ninguna cinta para reproducir.");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && reproduciendo)
        {
            if (!pausado)
                Pausar();
            else
                Reanudar();
        }
    }

    void ReproducirVHS()
    {
        cintaVHS.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cinematica.Play();
        reproduciendo = true;
        CineReproduciendo = true;
        audioManager.Stop("MainTheme");
        cinematica.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        puerta.VHSVisto();
        cintaVHS.SetActive(false);
        reproduciendo = false;
        CineReproduciendo = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pausar()
    {
        Debug.Log("Pausar llamada"); // Mensaje de depuración
        cinematica.Pause();
        panelPausa.SetActive(true);
        Debug.Log("Panel de pausa activado"); // Mensaje de depuración
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pausado = true;
    }

    public void Reanudar()
    {
        Debug.Log("Reanudar llamada"); // Mensaje de depuración
        cinematica.Play();
        panelPausa.SetActive(false);
        Debug.Log("Panel de pausa desactivado"); // Mensaje de depuración
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausado = false;
    }

    public void SaltarCinematica()
    {
        Debug.Log("SaltarCinematica llamada"); // Mensaje de depuración
        cinematica.Pause();
        puerta.VHSVisto();
        cintaVHS.SetActive(false);
        reproduciendo = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = false;
            panelNoVHS.SetActive(false);
        }
    }
}
