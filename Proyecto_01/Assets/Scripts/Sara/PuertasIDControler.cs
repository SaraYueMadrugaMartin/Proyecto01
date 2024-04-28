using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasIDControler : MonoBehaviour
{
    [SerializeField] private GameObject panelPregunta;
    public static bool destruye = false;

    public void UsarLlave()
    {
        destruye = true;
        panelPregunta.SetActive(false);
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
    }
}
