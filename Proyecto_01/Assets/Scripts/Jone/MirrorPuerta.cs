using BBUnity.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MirrorPuerta : MonoBehaviour
{
    static Animator anim;
    [SerializeField] GameObject puerta;
    [SerializeField] GameObject panelAviso;
    [SerializeField] TextMeshProUGUI textoAviso;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void VHSVisto()
    {
        anim.SetBool("roto", true);
        ActivarPuerta();
    }

    private void ActivarPuerta()
    {
        puerta.SetActive(true);
        textoAviso.text = "Solo podré cruzar el espejo\r\nuna vez, debo tener cuidado.";
        StartCoroutine(EsperaSegundos());
    }

    IEnumerator EsperaSegundos()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        panelAviso.SetActive(true);
        yield return new WaitForSecondsRealtime(3.5f);
        panelAviso.SetActive(false);
    }
}
