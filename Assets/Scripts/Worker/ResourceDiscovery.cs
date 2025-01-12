using System;
using UnityEngine;

public class ResourceDiscovery : MonoBehaviour
{
    public event Action<Resource> Discovered;

    private Worker _worker;

    private void OnTriggerEnter(Collider other)
    {
        var offsetY = new Vector3(0, other.transform.position.y, 0);

        if (other.TryGetComponent(out ICollectable resource)
            && other.transform.position - offsetY == _worker.DestinationPoint)
        { 
            Discovered?.Invoke((Resource)resource);
        }
    }

    public void Initialize(Worker worker) => _worker = worker;
}
