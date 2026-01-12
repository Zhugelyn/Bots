using System;
using UnityEngine;
using Workers;

public class ResourceReceiver : MonoBehaviour
{
    public event Action<Resource> ResourceAccepted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Worker worker))
            if (worker.Resource != null)
            {
                Resource resource = worker.Resource;
                worker.DropResource();
                ResourceAccepted?.Invoke(resource);
            }
    }
}
