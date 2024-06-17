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
    [SerializeField] Slider brightnessSlider1;
    [SerializeField] Slider brightnessSlider2;
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
                brightnessSlider1.onValueChanged.AddListener(AdjustBrightness);
                brightnessSlider2.onValueChanged.AddListener(AdjustBrightness);
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
            brightnessSlider1.value = savedBrightness;
            brightnessSlider2.value = savedBrightness;
            colorAdjustments.postExposure.value = savedBrightness;
        }
    }
}
