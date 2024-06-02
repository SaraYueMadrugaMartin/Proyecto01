using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoEncenderLuces : MonoBehaviour
{
    private bool unaVez = true;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {          
            Debug.Log("se cumple");
            CambiaLuces();
        }
    }

    private void CambiaLuces()
    {
        if (unaVez) // Para que solo haga el cambio una vez
        {
            LucesTodas luces = GameObject.Find("GlobalLight").GetComponent<LucesTodas>();
            luces.CambiaEstadoLuces();
            unaVez = false;
        }
        StartCoroutine(EsperaUnPoco());
    }

    // Vuelve a poner la bandera en true después de esperar
    private IEnumerator EsperaUnPoco()
    {
        yield return new WaitForSecondsRealtime(3f);
        unaVez = true;
    }
}
