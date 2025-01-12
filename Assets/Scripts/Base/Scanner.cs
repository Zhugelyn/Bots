using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const string NameLayerMask = "Resource";

    private float _radius;
    private LayerMask _layerMask;
    private int _scanDelay;

    public event Action<List<Vector3>> ResourcesFound;

    public void Initialize()
    {
        _radius = 150;
        _scanDelay = 10;
        _layerMask = LayerMask.GetMask(NameLayerMask);

        StartCoroutine(StartScan());
    }

    private void Start()
    {
        Initialize();
    }

    private IEnumerator StartScan()
    {
        var wait = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            var resources = Scan();

            if (resources.Count > 0)
                ResourcesFound?.Invoke(resources);

            yield return wait;
        }
    }

    private List<Vector3> Scan()
    {
        List<Vector3> resourcesPosition = new List<Vector3>();
        var resources = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        foreach (var resourc in resources)
        {
            Resource resource = resourc.GetComponent<Resource>();

            if (resource.IsScaned == false)
            {
                resourcesPosition.Add(resource.transform.position);
                resource.IsScaned = true;
            }
        }

        return resourcesPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
