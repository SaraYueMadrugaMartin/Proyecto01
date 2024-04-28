using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasIDControler : MonoBehaviour
{
    [SerializeField] private PlantillaPuertas puertaAsociada;
    [SerializeField] private test llaveAsociada;

    public void CompararIDs()
    {
        if(puertaAsociada.puertasID == llaveAsociada.ID)
        {
            Debug.Log("La llave es correcta.");
        }
        else
        {
            Debug.Log("La llave no es la correcta. Necesitas otra.");
        }
    }
}
