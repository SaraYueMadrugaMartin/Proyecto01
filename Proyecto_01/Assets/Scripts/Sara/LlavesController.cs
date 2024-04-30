using System.Collections.Generic;
using UnityEngine;

public class LlavesController : MonoBehaviour
{
    [SerializeField] private PlantillaLlaves llaveAsociada;
    public static int llaveID;

    private void Start()
    {
        llaveID = ObtenerID();
    }
    public int ObtenerID()
    {
        return llaveAsociada.devuelveID();
    }
}
