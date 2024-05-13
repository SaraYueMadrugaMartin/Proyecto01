using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "ScriptableObjects/Doors")]

public class PlantillaPuertas : ScriptableObject
{
    public int puertasID;
    public bool puertaBloqueada = true;

    public int idPuertas()
    {
        return puertasID;
    }
}
