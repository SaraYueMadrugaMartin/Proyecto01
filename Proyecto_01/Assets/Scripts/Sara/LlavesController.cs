using System.Collections.Generic;
using UnityEngine;

public class LlavesController : MonoBehaviour
{
    [SerializeField] private int llaveID; // Cambia a privado o protegido si es necesario

    public int ObtenerID()
    {
        return llaveID;
    }

}
