using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludAnim : MonoBehaviour
{
    Animator anim;
    SFXManager sfxManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sfxManager = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("salud", Player.saludActual);
        //sfxManager.PlaySFX(sfxManager.corazonLatir);
    }
}
