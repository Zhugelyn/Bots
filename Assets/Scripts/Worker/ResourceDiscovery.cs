using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ResourceDiscovery : MonoBehaviour
{
    public event Action<Resource> Discovered;

    private Worker _worker;

    public void Initialize(Worker worker)
    {
        _worker = worker;
    }

    private void OnTriggerEnter(Collider other)
    {
        var offsetY = new Vector3(0, other.transform.position.y, 0);

        if (other.TryGetComponent(out Resource resource)
            && other.transform.position - offsetY == _worker.DestinationPoint)
        { 
            resource.Subscribe(_worker);
            Discovered?.Invoke(resource);
        }
    }
}
