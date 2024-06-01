using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class XelaHealthAnim : MonoBehaviour
{
    static Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = Xela.saludActual;
    }

    private void Update()
    {
        
    }

    public static void CambiaValue()
    {
        slider.value = Xela.saludActual;
    }
}