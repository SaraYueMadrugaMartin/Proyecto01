using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraCorrupcion : MonoBehaviour
{
    public Slider slider;

    public void Corrupcion(int corrupcion)
    {
        slider.value = corrupcion;
    }
}
