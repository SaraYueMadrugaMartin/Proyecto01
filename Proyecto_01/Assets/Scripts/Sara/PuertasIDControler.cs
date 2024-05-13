using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasIDControler : MonoBehaviour
{
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Inventario inventario;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject player;

    [SerializeField] private Vector2 posicionTransicion = new Vector2(8.51f, 0.51f);
    [SerializeField] private Vector2 posicionTransicion02;

    public static bool destruye = false;

    public void UsarLlave()
    {
        Invoke("CambioPosicionPlayer", 0.3f);
        fadeAnimation.FadeOut();
        destruye = true;
        panelPregunta.SetActive(false);
        inventario.VaciarHueco("Llave");
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
    }

    public void CambioPosicionPlayer()
    {
        if (inventario.TieneObjeto("Llave"))
            player.transform.position = posicionTransicion;
        else if (inventario.TieneObjeto("Fusible"))
            player.transform.position = posicionTransicion02;
    }

    public void NotificarDestruccionPuerta(Puerta puerta)
    {
        DestruirPuerta(puerta.idPuerta);
    }

    public void DestruirPuerta(int idPuerta)
    {
        // Encuentra la puerta con el ID correspondiente
        GameObject puertaObjeto = GameObject.Find("Puerta" + idPuerta);
        if (puertaObjeto != null)
        {
            // Obtén todos los colliders asociados a la puerta
            Collider2D[] collidersPuerta = puertaObjeto.GetComponents<Collider2D>();

            // Desactiva todos los colliders
            foreach (Collider2D colliderPuerta in collidersPuerta)
            {
                colliderPuerta.enabled = false;
                Debug.Log("Collider de la puerta " + idPuerta + " desactivado.");
            }
        }
        else
        {
            Debug.LogWarning("La puerta con el ID " + idPuerta + " no fue encontrada.");
        }
    }
}
