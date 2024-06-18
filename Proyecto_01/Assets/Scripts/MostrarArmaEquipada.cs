using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarArmaEquipada : MonoBehaviour
{
    static Image imageArmaBate, imageArmaPistola;
    public static int armaEquipada = 0;
    public Image[] imagenesArmas;

    void Start()
    {
        imageArmaBate = transform.Find("BateEquipado").GetComponent<Image>();
        imageArmaPistola = transform.Find("PistolaEquipado").GetComponent<Image>();
        imagenesArmas = GetComponentsInChildren<Image>();
        imageArmaBate.enabled = false;
        imageArmaPistola.enabled = false;
    }

    public static void ArmaEquipada(int arma)
    {
        switch (arma)
        {
            case 0:
                imageArmaBate.enabled = false;
                imageArmaPistola.enabled = false;
                armaEquipada = 0;
                break;
            case 1:
                imageArmaBate.enabled = true;
                imageArmaPistola.enabled = false;
                armaEquipada = 1;
                break;
            case 2:
                imageArmaBate.enabled = false;
                imageArmaPistola.enabled = true;
                armaEquipada = 2;
                break;
        }
    }

    public static int GetMostrarArmaEquipada()
    {
        return armaEquipada;
    }

    public static void SetMostrarArmaEquipada(int arma)
    {
        armaEquipada = arma;
    }
}
