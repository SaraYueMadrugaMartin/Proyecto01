using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CorrAnim : MonoBehaviour
{
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = Mathf.Clamp(Player.corrupcion/100, 0f, 1f);
    }
}
