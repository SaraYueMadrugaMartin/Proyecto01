using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasIDControler : MonoBehaviour
{
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Inventario inventario;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cadenas;
    [SerializeField] private Vector2 posicionTransicion02;

    public static bool destruye = false;

    private Puerta puertaActual;

    SFXManager sfxManager;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
    }

    public void UsarLlave()
    {
        Debug.Log("Click S�");

        puertaActual.ActualizarEstadoPuerta();

        if (!puertaActual.puertaAsociada.puertaBloqueada)
        {
            
            panelPregunta.SetActive(false);
            Puerta.panelAbierto = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            NotificarDestruccionPuerta(puertaActual);

            if (puertaActual.idPuerta != 3)
            {
                sfxManager.PlaySFX(sfxManager.clipsDeAudio[13]);
                fadeAnimation.FadeOut();
                puertaActual.CambioPosicionPlayer();
            }
            else
            {
                sfxManager.PlaySFX(sfxManager.clipsDeAudio[20]); 
                fadeAnimation.FadeOut();
                cadenas.SetActive(false);
                puertaActual.CambioPosicionPlayer();
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }        
    }

    public void SetPuertaActual(Puerta puerta)
    {
        puertaActual = puerta;
    }

    public void NoUsarLlave()
    {
        Debug.Log("Click NO");
        panelPregunta.SetActive(false);
        Puerta.panelAbierto = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void NotificarDestruccionPuerta(Puerta puerta)
    {
        DestruirPuerta(puerta.idPuerta);
        puerta.DesactivarColliders();
        puerta.ActivarPuertaSinLlave();
    }

    public void DestruirPuerta(int idPuerta)
    {
        // Encuentra la puerta con el ID correspondiente
        GameObject puertaObjeto = GameObject.Find("Puerta" + idPuerta);
        if (puertaObjeto != null)
        {
            Collider2D[] collidersPuerta = puertaObjeto.GetComponents<Collider2D>();

            // Desactiva todos los colliders
            foreach (Collider2D colliderPuerta in collidersPuerta)
            {
                colliderPuerta.enabled = false;
                Debug.Log("Collider de la puerta " + idPuerta + " desactivado.");
            }
        }
    }

    #region Metodos para el Save System
    public bool GetCadenasActivadas()
    {
        return cadenas.activeSelf;
    }

    public void SetCadenasActivadas(bool value)
    {
        cadenas.SetActive(value);
    }
    #endregion
}
