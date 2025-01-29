using System;
using UnityEngine;

public class ResourceReceiver : MonoBehaviour
{
    public event Action<Resource> ResourceAccepted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Worker worker))
            if (worker.Resource != null)
            {
                Resource resource = worker.Resource;
                resource.Unsubscribe();
                worker.SetIdleState();
                ResourceAccepted?.Invoke(resource);
            }
    }
}
