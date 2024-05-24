using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/IDsItems")]

public class IDsItems : ScriptableObject
{
    public int IDsItem;

    public int GetIDsItem()
    {
        return IDsItem;
    }

    public void SetIDsItem(int newID)
    {
        IDsItem = newID;
    }
}
