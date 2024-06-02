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
    [SerializeField] GameObject panelMirror;

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
        StartCoroutine(EsperaSegundos());
    }

    IEnumerator EsperaSegundos()
    {
        yield return new WaitForSecondsRealtime(1f);
        panelMirror.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        panelMirror.SetActive(false);
    }
}
