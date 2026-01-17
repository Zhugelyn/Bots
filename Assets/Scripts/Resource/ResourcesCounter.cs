using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesCounter : MonoBehaviour
{
    [SerializeField] private ResourceReceiver _resourceReceiver;
    
    private Dictionary<ResourceType, int> _resourcesCount;
    
    public event Action<Dictionary<ResourceType, int>> Changed;

    private void Awake()
    {
        _resourcesCount = new Dictionary<ResourceType, int>();
    }

    private void OnEnable()
    {
        _resourceReceiver.ResourceAccepted += ChangeResourceCount;
    }

    private void OnDisable()
    {
        _resourceReceiver.ResourceAccepted -= ChangeResourceCount;
    }
    
    private void ChangeResourceCount(Resource resource)
    {
        int startResourceCount = 1;
        ResourceType type = resource.Type;
        var needType = _resourcesCount.Where(r => r.Key == type);

        if (needType.Any())
            _resourcesCount[type]++;
        else
            _resourcesCount.Add(resource.Type, startResourceCount);
        
        Changed?.Invoke(_resourcesCount);
    }
}
