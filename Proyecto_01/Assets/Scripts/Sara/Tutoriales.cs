using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutoriales : MonoBehaviour
{
    [SerializeField] private GameObject panelTutoriales;
    private Collider2D collidersTuto;
    private bool colliderDesactivado = false;

    void Start()
    {
        collidersTuto = GetComponent<Collider2D>();
        panelTutoriales.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !colliderDesactivado)
        {
            Time.timeScale = 0f;
            panelTutoriales.SetActive(true);
        }
    }

    public void SalirTuto()
    {
        panelTutoriales.SetActive(false);
        collidersTuto.enabled = false;
        colliderDesactivado = true;
        Time.timeScale = 1f;
    }
}
