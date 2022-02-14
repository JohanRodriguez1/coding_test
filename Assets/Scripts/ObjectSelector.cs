using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TestAnimares;

public class ObjectSelector : MonoBehaviour, IPointerClickHandler 
{


    [SerializeField] private SystemID systemPlanets;
    public SystemID SystemPlanet
    {
        get { return systemPlanets; }
    }

    private Vector3 initialPosition;
    public Vector3 InitialPosition
    {
        get { return initialPosition; }
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    public Action<Transform> EventPlanetSelected = delegate { }; 

    public void OnPointerClick(PointerEventData eventData)
    {
        EventPlanetSelected(transform);
    }
    
}
