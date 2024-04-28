using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Keys")]

public class PlantillaLlaves : ScriptableObject
{
    public int ID;
    
    public int devuelveID()
    {
        return ID;
    }
}
