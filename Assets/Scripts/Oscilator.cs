using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) //epsilon is smallest float possible, better to use this instead of !=0 for floats 
        {
            return;   //code after this wont be executed 
        }

        Oscilate();

    }

    void Oscilate()
    {
        float cycles = Time.time / period;  //constantly growing over time 

        const float tau = Mathf.PI * 2;  //const value of 6.2873 (tau = 2*pi)

        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;  //recalulated to go from 0 to 1 so its cleaner. else starting point would be midpoint of movement

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}
