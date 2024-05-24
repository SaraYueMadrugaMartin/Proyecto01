using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/IDsItems")]

public class IDsItems : ScriptableObject
{
    public string IDsItem;

    public string GetIDsItem()
    {
        return IDsItem;
    }

    public void SetIDsItem(string newID)
    {
        IDsItem = newID;
    }
}
