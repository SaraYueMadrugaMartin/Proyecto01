using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cinematica : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    [SerializeField] GameObject panelPausa;

    private bool pausado = false;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        videoPlayer.loopPointReached += OnVideoEnd;
        panelPausa.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausado)
                Pausar();
            else
                Reanudar();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {        
        CambioEscena();
    }

    public void CambioEscena()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;
        SceneManager.LoadScene(siguienteEscena);
    }

    private void Pausar()
    {
        videoPlayer.Pause();
        panelPausa.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pausado = true;
    }

    public void Reanudar()
    {
        videoPlayer.Play();
        panelPausa.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausado = false;
    }
}
