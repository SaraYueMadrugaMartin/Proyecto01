using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuzRota : MonoBehaviour
{
    private Light2D luz;

    private int frames = 0;

    [SerializeField] private int framesPerRandomize = 120;
    [SerializeField] private float minValue = 0;
    [SerializeField] private float maxValue = 2;

    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames % framesPerRandomize == 0)
        {
            RandomizeIntensity();
        }
    }

    void RandomizeIntensity()
    {
        System.Random random = new System.Random();

        float randomValue = (float)(random.NextDouble() * (maxValue - minValue) + minValue);

        luz.intensity = randomValue;
    }
}
