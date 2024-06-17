using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSliderMusica;
    [SerializeField] private Slider volumeSliderEfectos;

    private void Start()
    {
        // Cargar el volumen guardado al iniciar
        if (AudioManager.instance != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("VolumenGeneral", 1.0f);
            volumeSliderMusica.value = savedVolume;
            AudioManager.instance.ActualizarVolumenGeneral(savedVolume);
        }

        if (SFXManager.instance != null)
        {
            float savedVolumeSFX = PlayerPrefs.GetFloat("VolumenGeneral", 1.0f);
            volumeSliderEfectos.value = savedVolumeSFX;
            SFXManager.instance.ActualizarVolumenEfectos(savedVolumeSFX);
        }

        // Añadir listener al slider para cambiar el volumen
        volumeSliderMusica.onValueChanged.AddListener(HandleSliderValueChangedGeneral);
        volumeSliderEfectos.onValueChanged.AddListener(HandleSliderValueChangedEfectos);
    }

    private void HandleSliderValueChangedGeneral(float value)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ActualizarVolumenGeneral(value);
        }
    }
    private void HandleSliderValueChangedEfectos(float value)
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlaySFX(SFXManager.instance.clipsDeAudio[0]);
            SFXManager.instance.ActualizarVolumenEfectos(value);
        }
    }
}
