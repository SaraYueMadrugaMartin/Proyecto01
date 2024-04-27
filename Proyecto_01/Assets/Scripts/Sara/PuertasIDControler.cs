using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasIDControler : MonoBehaviour
{
    [SerializeField] private int puertaID; // Cambia a privado o protegido si es necesario

    public int ObtenerID()
    {
        return puertaID;
    }
}
