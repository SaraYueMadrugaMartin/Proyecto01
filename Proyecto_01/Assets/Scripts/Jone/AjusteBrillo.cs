using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AjusteBrillo : MonoBehaviour
{
    public Volume postProcessingVolume;
    public Slider brightnessSlider;

    private ColorAdjustments colorAdjustments;

    void Start()
    {
        if (postProcessingVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            brightnessSlider.onValueChanged.AddListener(AdjustBrightness);
        }
        else
        {
            Debug.LogError("Color Adjustments not found in the Volume Profile.");
        }
    }

    void AdjustBrightness(float value)
    {
        // El rango del slider debe estar configurado entre -100 y 100 para un control adecuado del brillo.
        colorAdjustments.postExposure.value = value;
    }
}
