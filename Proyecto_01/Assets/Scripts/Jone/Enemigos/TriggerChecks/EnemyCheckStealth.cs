using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckStealth : MonoBehaviour
{
    [SerializeField] GameObject triggerNormal;
    [SerializeField] GameObject triggerStealth;
    void Update()
    {
        if (Player.estaSigilo)
        {
            triggerNormal.SetActive(false);
            triggerStealth.SetActive(true);
        }
        else
        {
            triggerNormal.SetActive(true);
            triggerStealth.SetActive(false);
        }
    }
}
