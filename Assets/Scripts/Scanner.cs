using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const string ResourceLayerMask = "Resource";
    private const int BufferSize = 64;

    private float _radius = 150;
    private int _scanDelay = 15;
    private LayerMask _layerMask;
    private Collider[] _buffer = new Collider[BufferSize];

    public event Action<IReadOnlyList<Resource>> ResourcesFound;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask(ResourceLayerMask);
        StartCoroutine(StartScan());
    }

    private IEnumerator StartScan()
    {
        var wait = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            List<Resource> resources = Scan();

            if (resources.Count > 0)
                ResourcesFound?.Invoke(resources);

            yield return wait;
        }
    }

    private List<Resource> Scan()
    {
        List<Resource> found = new List<Resource>();
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _buffer, _layerMask);

        for (int i = 0; i < count; i++)
        {
            if (_buffer[i].TryGetComponent(out Resource resource) == false)
                continue;

            if (resource.transform.parent != null)
                continue;

            found.Add(resource);
        }

        return found;
    }
}
