using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChecker : MonoBehaviour
{
    [SerializeField] private  BoxCollider _placementZone;
    [SerializeField] private List<string> _layerMasks = new List<string>();
    private float _tresholdDistance = 10f;
    private LayerMask _mask;
    
    private void Start()
    {
        _mask = LayerMask.GetMask(_layerMasks.ToArray());
    }

    public bool CanPlaced(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _tresholdDistance, _mask);
        
        if (!_placementZone.bounds.Contains(position) || colliders.Length > 0)
            return false;
        
        return true;
    }
}
