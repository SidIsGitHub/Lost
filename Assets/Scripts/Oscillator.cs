using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f; //basically the speed
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float cycles = Time.time / period; //continually increases over time.
        const float tau = Mathf.PI * 2; // constant value of 6.283...(Pi * 2).
        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1.

        movementFactor = (rawSinWave + 1f) / 2f; //dividing such that the values range from 0 to 1.

        Vector3 offset = movementVector * movementFactor; //calculating offset 
        transform.position = startingPosition + offset; //finalising position of gameobject to total offset (variable).
    }
}
