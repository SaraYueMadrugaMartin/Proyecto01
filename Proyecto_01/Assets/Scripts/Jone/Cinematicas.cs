using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cinematicas : MonoBehaviour
{
    public static Cinematicas Instance; // Singleton

    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject cintaVHS;
    [SerializeField] private GameObject cintaFinBueno;
    [SerializeField] private GameObject cintaFinMalo;
    private GameObject cinta;
    private VideoPlayer cinematica;

    private bool pausado = false;

    public static bool CineReproduciendo { get; set; } = false;

    [SerializeField] GameObject puertaGO;
    MirrorPuerta puerta;

    private void Awake()
    {
        // Estructura singleton
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
        cintaVHS.SetActive(false);
        panelPausa.SetActive(false);
        puerta = puertaGO.GetComponent<MirrorPuerta>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CineReproduciendo)
        {
            if (!pausado)
                Pausar();
            else
                Reanudar();
        }
    }

    public void Reproducir(int cinem)
    {
        switch (cinem)
        {
            case 0:
                cinta = cintaVHS;
                break;
            case 1:
                cinta = cintaFinBueno;
                break;
            case 2:
                cinta = cintaFinMalo;
                break;
        }
        cinematica = cinta.GetComponent<VideoPlayer>();

        cinta.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cinematica.Play();
        CineReproduciendo = true;
        AudioManager.instance.StopAllSounds();
        cinematica.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        cinta.SetActive(false);
        puerta.VHSVisto();
        Debug.Log("puerta abre");
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
        cinta.SetActive(false);
        CineReproduciendo = false;
        panelPausa.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
