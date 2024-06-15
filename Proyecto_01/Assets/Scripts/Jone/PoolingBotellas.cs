using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingBotellas : MonoBehaviour
{
    public static PoolingBotellas instancia;
    public GameObject botellaPrefab;
    [SerializeField] int cantidad = 4;

    private List<GameObject> botellas;

    private void Awake()
    {
        instancia = this;
        botellas = new List<GameObject>(cantidad);
        for (int i = 0; i < cantidad; i++)
        {
            GameObject prefabInstancia = Instantiate(botellaPrefab);
            prefabInstancia.transform.SetParent(transform);
            prefabInstancia.SetActive(false);
            botellas.Add(prefabInstancia);
        }
    }

    public GameObject GetBotella()
    {
        int totalBotellas = botellas.Count;

        for (int i = 0; i < totalBotellas; i++)
        {
            if (!botellas[i].activeInHierarchy)
            {
                botellas[i].SetActive(true);
                return botellas[i];
            }
        }
        return null;
    }
}
