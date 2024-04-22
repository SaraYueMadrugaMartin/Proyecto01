using System.Collections.Generic;
using UnityEngine;

public class LlavesController : MonoBehaviour
{
    // Definir una estructura de datos para almacenar la relación entre llaves y puertas
    [System.Serializable]
    public struct LlavePuerta
    {
        public GameObject llave;
        public GameObject puerta;
    }

    public List<LlavePuerta> llavesPuertas = new List<LlavePuerta>();

    // Método para intentar abrir una puerta con una llave dada
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
        Debug.Log("No se encontró una puerta asociada a esta llave.");
    }
}
