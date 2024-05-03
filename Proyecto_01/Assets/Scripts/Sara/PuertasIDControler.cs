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
}
