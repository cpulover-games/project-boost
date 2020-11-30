using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Positioning : MonoBehaviour
{
    Vector3 startPosition;
    // range of movement
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2.0f; // second
    // curren position on range on scale 1 (0: not moved, 1: fully moved)
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        // Obstacle is child of Obstacles => use localPosition to match with inspector
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // static if period = 0
        if (period <= Mathf.Epsilon) return;

        movementFactor = Mathf.Abs(Mathf.Sin(Time.time / period * (Mathf.PI * 2)));
        Vector3 offset = movementVector * movementFactor;
        transform.localPosition = startPosition + offset;
    }
}
