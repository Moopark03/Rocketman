using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time / period;

        const float tau = Mathf.PI * 2f;

        float rawSinWave = Mathf.Sin(cycle * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        //Need movement factor 
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
