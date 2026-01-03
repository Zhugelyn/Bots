using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const string  ResourceLayerMask = "Resource";
    
    public event Action<List<Vector3>> ResourcesFound;

    private float _radius;
    private LayerMask _layerMask;
    private int _scanDelay;

    private HashSet<Vector3> _scannedResources;

    public void Initialize()
    {
        _radius = 150;
        _scanDelay = 10;
        _layerMask = LayerMask.GetMask(ResourceLayerMask);
        _scannedResources = new HashSet<Vector3>();
        
        StartCoroutine(StartScan());
    }

    private void Awake()
    {
        Initialize();
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
            
            Vector3 position = collider.transform.position; 
            
            if (_scannedResources.Contains(position))
                continue;

            resourcesPosition.Add(position);
            _scannedResources.Add(position);
        }

        return resourcesPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}