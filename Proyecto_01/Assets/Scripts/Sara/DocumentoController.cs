using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.UI;
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980

public class DocumentoController : MonoBehaviour
{
    [SerializeField] private GameObject mensajeDoc;

<<<<<<< HEAD
    private bool jugadorTocando = false;

    // Start is called before the first frame update
=======
    public bool jugadorTocando = false;
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
    void Start()
    {
        
    }

<<<<<<< HEAD
    // Update is called once per frame
=======
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
    void Update()
    {
        if(jugadorTocando && Input.GetKeyDown("e"))
        {
<<<<<<< HEAD
=======
            Time.timeScale = 0;
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
            mensajeDoc.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = false;
        }
    }

    public void SalirDocumento()
    {
<<<<<<< HEAD
=======
        Time.timeScale = 1;
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
        mensajeDoc.SetActive(false);
    }
}
