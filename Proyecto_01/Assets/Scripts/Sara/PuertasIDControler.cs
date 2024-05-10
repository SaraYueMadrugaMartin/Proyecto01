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
        player.transform.position = posicionTransicion;
    }

    public void NotificarDestruccionPuerta(Puerta puerta)
    {
        DestruirPuerta(puerta.idPuerta);
    }

    public void DestruirPuerta(int idPuerta)
    {
        // Encuentra la puerta con el ID correspondiente y desactívala
        GameObject puertaADestruir = GameObject.Find("Puerta" + idPuerta);
        if (puertaADestruir != null)
        {
            puertaADestruir.SetActive(false);
            Debug.Log("Puerta " + idPuerta + " destruida.");
        }
        else
        {
            Debug.LogWarning("La puerta con el ID " + idPuerta + " no fue encontrada.");
        }
    }
}
