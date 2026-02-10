using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const string ResourceLayerMask = "Resource";
    
    private float _radius = 150;
    private int _scanDelay = 10;
    private LayerMask _layerMask;
    
    public event Action<List<Vector3>> ResourcesFound;
    
    private void Awake()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        _layerMask = LayerMask.GetMask(ResourceLayerMask);
        
        StartCoroutine(StartScan());
    }

    private IEnumerator StartScan()
    {
        var wait = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            List<Vector3> resources = Scan();

            if (resources.Count > 0)
                ResourcesFound?.Invoke(resources);

            yield return wait;
        }
    }

    private  List<Vector3> Scan()
    {
        List<Vector3> resourcesPosition = new List<Vector3>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Resource _) == false)
                continue;
            
            resourcesPosition.Add(collider.transform.position);
        }

        return resourcesPosition;
    }
}