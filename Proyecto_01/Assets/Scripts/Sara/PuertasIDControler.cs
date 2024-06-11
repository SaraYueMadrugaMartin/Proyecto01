using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasIDControler : MonoBehaviour
{
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Inventario inventario;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private Puerta[] puertas;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cadenas;
    [SerializeField] private Vector2 posicionTransicion02;

    public static bool destruye = false;

    SFXManager sfxManager;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
    }

    public void UsarLlave()
    {
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[13]);        
        destruye = true;
        panelPregunta.SetActive(false);
        inventario.VaciarHueco("Llave");

        for(int i = 0; i < puertas.Length; i++)
        {
            if (!puertas[i].puertaBloqueada)
            {
                if(puertas[i].idPuerta != 3)
                {
                    fadeAnimation.FadeOut();
                    puertas[i].CambioPosicionPlayer();
                    Debug.Log("Cambia posicion");
                }
                else
                {
                    fadeAnimation.FadeOut();
                    cadenas.SetActive(false);
                    puertas[i].CambioPosicionPlayer();
                    Debug.Log("Esta es la puerta 3, no hay cambio de posición");
                }
            }
        }
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
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
}
