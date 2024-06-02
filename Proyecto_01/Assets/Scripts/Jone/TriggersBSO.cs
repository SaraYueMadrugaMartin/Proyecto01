using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersBSO : MonoBehaviour
{
    [SerializeField] string nombreCancion;  // Nombre del audio asociado a la zona

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Play(nombreCancion);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Stop(nombreCancion);
        }
    }
}
