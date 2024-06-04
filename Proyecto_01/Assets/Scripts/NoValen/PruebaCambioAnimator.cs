using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaCambioAnimator : MonoBehaviour
{
    Animator anim;
    [SerializeField] RuntimeAnimatorController animatorController1;
    [SerializeField] RuntimeAnimatorController animatorController2;
    bool cambia = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CambiaAnimator(cambia);
            cambia = !cambia;
        }
    }

    private void CambiaAnimator(bool cambia)
    {
        if (cambia)
        {
            anim.runtimeAnimatorController = animatorController1;
        }
        else
        {
            anim.runtimeAnimatorController = animatorController2;
        }
    }
}
