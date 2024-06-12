using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AjusteBrillo : MonoBehaviour
{
    public static AjusteBrillo Instance { get; private set; }

    public Volume postProcessingVolume;
    public Slider brightnessSlider;
    private ColorAdjustments colorAdjustments;
    private const string BrightnessKey = "Brightness";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (postProcessingVolume == null)
            {
                postProcessingVolume = FindObjectOfType<Volume>();
            }

            if (postProcessingVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
            {
                brightnessSlider.onValueChanged.AddListener(AdjustBrightness);
                LoadBrightness();
            }
            else
            {
                Debug.LogError("Color Adjustments not found in the Volume Profile.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void AdjustBrightness(float value)
    {
        colorAdjustments.postExposure.value = value;
        PlayerPrefs.SetFloat(BrightnessKey, value);
    }

    void LoadBrightness()
    {
        if (PlayerPrefs.HasKey(BrightnessKey))
        {
            float savedBrightness = PlayerPrefs.GetFloat(BrightnessKey);
            brightnessSlider.value = savedBrightness;
            colorAdjustments.postExposure.value = savedBrightness;
        }
    }
}
