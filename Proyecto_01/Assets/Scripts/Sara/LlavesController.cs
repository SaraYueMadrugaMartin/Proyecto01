using System.Collections.Generic;
using UnityEngine;

public class LlavesController : MonoBehaviour
{
    // Definir una estructura de datos para almacenar la relaci�n entre llaves y puertas
    [System.Serializable]
    public struct LlavePuerta
    {
        public GameObject llave;
        public GameObject puerta;
    }

    public List<LlavePuerta> llavesPuertas = new List<LlavePuerta>();

    // M�todo para intentar abrir una puerta con una llave dada
    public void IntentarAbrirPuerta(GameObject llave)
    {
        foreach (var relacion in llavesPuertas)
        {
            if (relacion.llave == llave)
            {
                relacion.puerta.GetComponent<Puerta>().ActualizarEstadoPuerta();
                return;
            }
        }
        Debug.Log("No se encontr� una puerta asociada a esta llave.");
    }
}
