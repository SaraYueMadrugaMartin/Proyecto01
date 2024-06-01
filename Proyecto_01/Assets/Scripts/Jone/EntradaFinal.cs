using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaFinal : MonoBehaviour
{
    [SerializeField] GameObject barrera;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter2D(Collider2D collision) // Cuando Alex entra
    {
        if (collision.CompareTag("Player"))
        {
            virtualCamera.m_Lens.OrthographicSize = 3f;
            Invoke("ActivaBarrera", 2f);
        }
    }

    private void ActivaBarrera()
    {
        barrera.SetActive(true);
    }
}
