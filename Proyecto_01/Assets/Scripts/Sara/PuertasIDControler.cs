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
    //[SerializeField] private Puerta puerta;

    //[SerializeField] private Vector2 posicionTransicion = new Vector2(8.51f, 0.51f);
    [SerializeField] private Vector2 posicionTransicion02;

    public static bool destruye = false;

    SFXManager sfxManager;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
    }

    public void UsarLlave()
    {
        //Invoke("CambioPosicionPlayer", 1f);
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[13], 0.5f);
        fadeAnimation.FadeOut();
        destruye = true;
        panelPregunta.SetActive(false);
        inventario.VaciarHueco("Llave");

        foreach (Puerta puerta in puertas)
        {
            if (puerta.idPuerta == 3 && !puerta.puertaBloqueada) // Comparamos que la puerta con ese ID sea la desbloqueada.
            {
                cadenas.SetActive(false);
                break;
            }
        }
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
    }

    /*public void CambioPosicionPlayer()
    {
        if (inventario.TieneObjeto("Llave"))
            player.transform.position = posicionTransicion;
        else if (inventario.TieneObjeto("Fusible"))
            player.transform.position = posicionTransicion02;
    }*/

    public void NotificarDestruccionPuerta(Puerta puerta)
    {
        DestruirPuerta(puerta.idPuerta);
        puerta.DesactivarColliders();
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
