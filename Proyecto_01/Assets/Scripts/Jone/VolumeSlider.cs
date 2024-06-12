using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        // Cargar el volumen guardado al iniciar
        if (AudioManager.instance != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("VolumenGeneral", 1.0f);
            volumeSlider.value = savedVolume;
            AudioManager.instance.ActualizarVolumenGeneral(savedVolume);
        }

        // Añadir listener al slider para cambiar el volumen
        volumeSlider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ActualizarVolumenGeneral(value);
        }
    }
}
