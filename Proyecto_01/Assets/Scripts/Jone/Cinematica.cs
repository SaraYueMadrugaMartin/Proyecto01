using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cinematica : MonoBehaviour
{
    VideoClip clip;
    private float duracion = 2f;
    
    void Start()
    {
        clip = GetComponent<VideoPlayer>().clip;
        duracion = (float)clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > (duracion - 30)) //Esto hay que cambiarlo está así para que sea más corto en las pruebas
        {
            CambioEscena();
        }      
    }

    private void CambioEscena()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;
        SceneManager.LoadScene(siguienteEscena);
    }
}
