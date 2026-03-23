using System;
using System.Collections.Generic;

public class ResourceProvider : UnityEngine.MonoBehaviour
{
    private readonly HashSet<Resource> _availableResources = new HashSet<Resource>();
    private readonly HashSet<Resource> _assignedResources = new HashSet<Resource>();

    public event Action<Resource> ResourceRemoved;

    public void AddResources(IReadOnlyList<Resource> resources)
    {
        foreach (var resource in resources)
        {
            if (_availableResources.Contains(resource) == false &&
                _assignedResources.Contains(resource) == false)
            {
                _availableResources.Add(resource);
            }
        }
    }

    public bool TryGetFreeResource(out Resource resource)
    {
        foreach (var candidate in _availableResources)
        {
            resource = candidate;
            _availableResources.Remove(candidate);
            _assignedResources.Add(candidate);
            return true;
        }

        resource = null;
        return false;
    }

    public void RemoveResource(Resource resource)
    {
        _availableResources.Remove(resource);
        _assignedResources.Remove(resource);
        ResourceRemoved?.Invoke(resource);
    }
}
