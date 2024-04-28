using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Sara/keys")]

public class PlantillaLlaves : ScriptableObject
{
    public int ID;
    
    public int devuelveID()
    {
        return ID;
    }
}
