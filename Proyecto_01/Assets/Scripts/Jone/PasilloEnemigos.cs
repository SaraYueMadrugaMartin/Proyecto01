using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasilloEnemigos : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int totalBalas = Player.contadorCorr;

        for (int i = 0; i < totalBalas; i++)
        {
            if (!balas[i].activeInHierarchy)
            {
                balas[i].SetActive(true);
                return balas[i];
            }
        }
        return null;
    }
}
