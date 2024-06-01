using BBUnity.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MirrorPuerta : MonoBehaviour
{
    static Animator anim;
    [SerializeField] GameObject puerta;

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
    }
}
