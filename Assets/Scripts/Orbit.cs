using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    [SerializeField] private Transform pivotTransform = null;
    [SerializeField] private float orbitVelocity;
    [SerializeField] private Vector3 rotationAxis;

    void Update()
    {
        transform.RotateAround(pivotTransform.position, rotationAxis, orbitVelocity * Time.deltaTime);
    }
}
