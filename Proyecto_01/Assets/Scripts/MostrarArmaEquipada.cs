using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarArmaEquipada : MonoBehaviour
{
    static GameObject armaBate, armaPistola;
    void Start()
    {
        armaBate = transform.Find("BateEquipado").gameObject;
        armaPistola = transform.Find("PistolaEquipado").gameObject;
        armaBate.SetActive(false);
        armaPistola.SetActive(false);
    }

    public static void ArmaEquipada(int arma)
    {
        switch (arma)
        {
            case 0:
                armaBate.SetActive(false);
                armaPistola.SetActive(false);
                break;
            case 1:
                armaBate.SetActive(true);
                armaPistola.SetActive(false);
                break;
            case 2:
                armaBate.SetActive(false);
                armaPistola.SetActive(true);
                break;
        }
    }
}
